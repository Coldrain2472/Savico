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

        public async Task UpdateGoalMonthlyContributionAsync(Goal goal)
        {
            goal.MonthlyContribution = CalculateMonthlyContribution(goal);
            context.Goals.Update(goal);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateGoalAsync(Goal updatedGoal)
        {
            var goal = await context.Goals.FindAsync(updatedGoal.Id);

            if (goal != null)
            {
                // updating goal properties
                goal.TargetAmount = updatedGoal.TargetAmount;
                goal.CurrentAmount = updatedGoal.CurrentAmount;
                goal.TargetDate = updatedGoal.TargetDate;
                goal.Description = updatedGoal.Description;

                // recalculating and updating the monthly contribution
                goal.MonthlyContribution = CalculateMonthlyContribution(goal);

                // saving the changes to the db
                context.Goals.Update(goal);
                await context.SaveChangesAsync();

                return true; // indicates that the update was successful
            }

            return false; // if the goal wasn't found
        }

        public async Task AddGoalAsync(GoalInputViewModel model, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            var userCurrency = user?.Currency;

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
                }) // calculats and sets the monthly contribution immediately ?
            };

            await context.Goals.AddAsync(goal);
            await context.SaveChangesAsync();
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

        // calculate the amount the user needs to save each month
        //public decimal CalculateMonthlySavings(decimal targetAmount, decimal currentAmount, DateTime targetDate)
        //{
        //    var monthsRemaining = (targetDate.Year - DateTime.Now.Year) * 12 + targetDate.Month - DateTime.Now.Month;

        //    if (monthsRemaining <= 0)
        //    {
        //        return 0; // if the target date is today or in the past, no monthly saving required
        //    }

        //    var requiredAmount = targetAmount - currentAmount;

        //    return requiredAmount / monthsRemaining;
        //}

        // soft delete the goal 
        public async Task DeleteGoalAsync(int goalId, string userId)
        {
            var goal = await context.Goals.FindAsync(goalId);

            if (goal != null && !goal.IsDeleted && goal.UserId == userId)
            {
                goal.IsDeleted = true;
                context.Goals.Update(goal);
                await context.SaveChangesAsync();
            }
        }

        // get all active goals for the user
        public async Task<IEnumerable<GoalViewModel>> GetAllGoalsAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            var currency = user!.Currency;

            var goals = await context.Goals
                .Where(g => g.UserId == userId && !g.IsDeleted)
                .Select(g => new GoalViewModel
                {
                    Id = g.Id,
                    TargetAmount = g.TargetAmount,
                    CurrentAmount = g.CurrentAmount,
                    TargetDate = g.TargetDate,
                    MonthlyContribution = g.MonthlyContribution,
                    Description = g.Description
                })
                .ToListAsync();

            return goals;
        }
    }
}
