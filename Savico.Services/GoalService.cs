namespace Savico.Services
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Savico.Core.Models;
    using Savico.Core.Models.ViewModels.Goal;
    using Savico.Infrastructure;
    using Savico.Services.Contracts;

    public class GoalService : IGoalService
    {
        private readonly SavicoDbContext context;
        private readonly UserManager<User> userManager;

        public GoalService(SavicoDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public decimal CalculateMonthlyContribution(Goal goal)
        {
            var remainingAmount = goal.TargetAmount - goal.CurrentAmount;

            var remainingMonths = (goal.TargetDate.Year - DateTime.Now.Year) * 12 + goal.TargetDate.Month - DateTime.Now.Month;

            if (remainingMonths > 0)
            {
                return remainingAmount / remainingMonths;
            }
            return 0; // if there are no remaining months, or the goal is already met
        }

		public decimal CalculateMonthlyGoalContribution(string userId, Goal goal)
		{
			var user = context.Users.Include(u => u.Budget).FirstOrDefault(u => u.Id == userId);
			if (user == null || user.Budget == null)
				return 0;

			var remainingAmount = goal.TargetAmount - goal.CurrentAmount;
			var remainingMonths = (goal.TargetDate.Year - DateTime.Now.Year) * 12 + goal.TargetDate.Month - DateTime.Now.Month;

			if (remainingMonths <= 0 || remainingAmount <= 0)
				return 0;

			// calculates how much the user can contribute each month towards the goal based on available budget
			var monthlyBudgetContribution = user.Budget.TotalAmount / remainingMonths;
			var goalContributionPerMonth = remainingAmount / remainingMonths;

			// returns the minimum of budget contribution or goal's required monthly contribution
			return Math.Min(monthlyBudgetContribution, goalContributionPerMonth);
		}

		public async Task UpdateGoalMonthlyContributionAsync(Goal goal)
        {
            goal.MonthlyContribution = CalculateMonthlyContribution(goal);

            context.Goals.Update(goal);

            await context.SaveChangesAsync();
        }

        public async Task UpdateGoalAsync(int goalId, GoalInputViewModel model, string userId)
        {
            var goal = await context.Goals
                .FirstOrDefaultAsync(g=>g.Id == goalId && g.UserId == userId);

            var user = await userManager.FindByIdAsync(userId);

            var userCurrency = user?.Currency;

            if (goal != null)
            {
                goal.Description = model.Description;
                goal.TargetDate = model.TargetDate;
                goal.CurrentAmount = model.CurrentAmount;
               // goal.MonthlyContribution = model.MonthlyContribution;
                goal.TargetAmount = model.TargetAmount;
                goal.MonthlyContribution = CalculateMonthlyContribution(goal);

                context.Goals.Update(goal);
                await context.SaveChangesAsync();
            }
        }

        public async Task AddGoalAsync(GoalInputViewModel model, string userId)
        {
            var user = await userManager.Users.Include(u => u.Budget).FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {
                var goal = new Goal
                {
                    UserId = userId,
                    TargetAmount = model.TargetAmount,
                    CurrentAmount = model.CurrentAmount,
                    TargetDate = model.TargetDate,
                    Description = model.Description,
                    MonthlyContribution = CalculateMonthlyContribution(new Goal
                    {
                        TargetAmount = model.TargetAmount,
                        CurrentAmount = model.CurrentAmount,
                        TargetDate = model.TargetDate
                    })
                };

                context.Goals.Add(goal);

                // Deduct the current amount from the user's available budget if applicable
                if (user.Budget != null && model.CurrentAmount > 0)
                {
                    user.Budget.TotalAmount -= model.CurrentAmount;

                    // Ensure that the user's budget changes are tracked
                    context.Users.Update(user);
                }

                await context.SaveChangesAsync();
            }
        }

        public async Task<GoalViewModel> GetGoalByIdAsync(int goalId, string userId)
        {
            var goal = await context.Goals
                .Where(g => g.UserId == userId && g.Id == goalId && !g.IsDeleted)
                .FirstOrDefaultAsync();

            var userCurrency = await context.Users
                .Where(u => u.Id == userId)
                .Select(u => u.Currency)
                .FirstOrDefaultAsync();

            if (goal != null && goal.UserId == userId)
            {
                var goalModel = new GoalViewModel
                {
                    Id = goal.Id,
                    TargetAmount = goal.TargetAmount,
                    CurrentAmount = goal.CurrentAmount,
                    TargetDate = goal.TargetDate,
                    Currency = userCurrency,
                    Description = goal.Description
                };

                return goalModel;
            }

            return null;
        }

        public async Task<GoalInputViewModel> GetGoalForEditAsync(int goalId, string userId)
        {
            var goal = await context.Goals
                .FirstOrDefaultAsync(g => g.Id == goalId && g.UserId == userId);

            if (goal == null)
            {
                return null;
            }

            var userCurrency = await context.Users
             .Where(u => u.Id == userId)
             .Select(u => u.Currency)
             .FirstOrDefaultAsync();

            var model = new GoalInputViewModel
            {
                Id = goal.Id,
                TargetAmount = goal.TargetAmount,
                CurrentAmount = goal.CurrentAmount,
                TargetDate = goal.TargetDate,
                Currency = userCurrency,
                Description = goal.Description,
                MonthlyContribution = goal.MonthlyContribution
            };

            return model;
        }

        // soft delete the goal 
        public async Task DeleteGoalAsync(int goalId, string userId)
        {
            var goal = await context.Goals.FindAsync(goalId);

            var user = await userManager.FindByIdAsync(userId);

            if (goal != null && !goal.IsDeleted && goal.UserId == userId && user != null)
            {
                // adds the current contributed amount back to the user's available budget
                user.Budget.TotalAmount += goal.CurrentAmount;

                // soft delete the goal
                goal.IsDeleted = true;
                context.Goals.Update(goal);

                // updating the user's budget
                context.Users.Update(user);

                await context.SaveChangesAsync();
            }
        }

        // get all active goals for the user
        public async Task<IEnumerable<GoalViewModel>> GetAllGoalsAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            var currency = user!.Currency;

			//        var goals = await context.Goals
			//            .Where(g => g.UserId == userId && !g.IsDeleted)
			//            .Select(g => new GoalViewModel
			//            {
			//                Id = g.Id,
			//                TargetAmount = g.TargetAmount,
			//                CurrentAmount = g.CurrentAmount,
			//                TargetDate = g.TargetDate,
			//                MonthlyContribution = g.MonthlyContribution,
			//                Description = g.Description,
			//	MonthlyGoalContribution = CalculateMonthlyGoalContribution(userId, g)
			//})
			//            .ToListAsync();

			//        return goals;

			var goals = await context.Goals
		.Where(g => g.UserId == userId && !g.IsDeleted)
		.ToListAsync(); // Get the goals from the DB first

			// Calculate MonthlyGoalContribution after fetching the data
			var goalViewModels = goals.Select(g => new GoalViewModel
			{
				Id = g.Id,
				TargetAmount = g.TargetAmount,
				CurrentAmount = g.CurrentAmount,
				TargetDate = g.TargetDate,
				//MonthlyContribution = g.MonthlyContribution,
				Description = g.Description,
				// Calculate MonthlyGoalContribution here, after fetching the data
				MonthlyGoalContribution = CalculateMonthlyGoalContribution(userId, g)
			}).ToList();

			return goalViewModels;
		}
    }
}
