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

        public async Task MarkGoalAsAchievedAsync(int goalId, string userId)
        {
            var goal = await context.Goals.FirstOrDefaultAsync(g => g.Id == goalId && g.UserId == userId);

            if (goal != null)
            {
                goal.IsAchieved = true;

                context.Goals.Update(goal);

                await context.SaveChangesAsync();
            }
        }

        public async Task ContributeToGoalAsync(int goalId, string userId, decimal contributionAmount)
        {
            var goal = await context.Goals.Include(g => g.User).ThenInclude(u => u.Budget)
                                           .FirstOrDefaultAsync(g => g.Id == goalId && g.UserId == userId);

            if (goal == null || goal.IsDeleted)
            {
                throw new InvalidOperationException("Goal not found.");
            }

            if (goal.IsAchieved)
            {
                throw new InvalidOperationException("Goal is already achieved.");
            }

            if (goal!.User!.Budget.TotalAmount >= contributionAmount)
            {
                // deducting the money from the available budget
                goal.User.Budget.TotalAmount -= contributionAmount;

                // updating the goal's current amount
                goal.CurrentAmount += contributionAmount;

                // check if goal is achieved after the contribution
                if (goal.CurrentAmount >= goal.TargetAmount)
                {
                    goal.IsAchieved = true;
                }

                context.Goals.Update(goal);
                context.Entry(goal.User.Budget).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Insufficient budget for the contribution.");
            }
        }

        public decimal CalculateMonthlyContribution(Goal goal)
        {
            var remainingAmount = goal.TargetAmount - goal.CurrentAmount;

            if (remainingAmount <= 0)
            {
                return 0; // no more contributions are needed, the goal is already achieved
            }

            var remainingMonths = (goal.TargetDate.Year - DateTime.Now.Year) * 12 + goal.TargetDate.Month - DateTime.Now.Month;

            if (remainingMonths <= 0)
            {
                return 0; // no time left for monthly contributions
            }

            return remainingAmount / remainingMonths;
        }

        public decimal CalculateMonthlyGoalContribution(string userId, Goal goal)
        {
            var user = context.Users.Include(u => u.Budget).FirstOrDefault(u => u.Id == userId);
            if (user == null || user.Budget == null)
                return 0;

            var remainingAmount = goal.TargetAmount - goal.CurrentAmount;
            var remainingDays = (goal.TargetDate - DateTime.Now).Days;
            var remainingMonths = (goal.TargetDate.Year - DateTime.Now.Year) * 12 + goal.TargetDate.Month - DateTime.Now.Month;

            // If the target date is today or in the past, no contribution is needed
            if (remainingAmount <= 0)
            {
                return 0;
            }

            // If no remaining months, fall back to remaining days
            if (remainingMonths <= 0)
            {
                if (remainingDays <= 0)
                {
                    return 0; // No time left for contributions
                }

                // Calculate based on remaining days
                decimal dailyContribution = remainingAmount / remainingDays;
                return dailyContribution * 30; // Estimate a monthly contribution based on the daily rate
            }

            // Calculate the monthly contribution to achieve the goal by the target date
            decimal requiredMonthlyContribution = remainingAmount / remainingMonths;

            // Now, calculate how much the user can contribute each month based on their available budget
            decimal availableMonthlyBudget = user.Budget.TotalAmount / remainingMonths;

            // The monthly contribution is the smaller of what is required and what the user can afford
            return Math.Min(availableMonthlyBudget, requiredMonthlyContribution);
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
                .FirstOrDefaultAsync(g => g.Id == goalId && g.UserId == userId);

            var user = await userManager.FindByIdAsync(userId);

            var userCurrency = user?.Currency;

            if (goal != null)
            {
                goal.Description = model.Description;
                goal.TargetDate = model.TargetDate;
                goal.CurrentAmount = model.CurrentAmount;
                goal.TargetAmount = model.TargetAmount;
                goal.MonthlyContribution = CalculateMonthlyContribution(goal);

                // mark as achieved if the current amount meets or exceeds the target amount
                if (goal.CurrentAmount >= goal.TargetAmount)
                {
                    goal.IsAchieved = true;
                }

                context.Goals.Update(goal);
                await context.SaveChangesAsync();
            }
        }

        public async Task AddGoalAsync(GoalInputViewModel model, string userId)
        {
            var user = await userManager.Users.Include(u => u.Budget)
                                   .FirstOrDefaultAsync(u => u.Id == userId);

            // current amount have to be always 0 at the beginning since the user hasnt started saving for the goal yet
            var newGoal = new Goal
            {
                UserId = userId,
                TargetAmount = model.TargetAmount,
                CurrentAmount = 0, 
                TargetDate = model.TargetDate,
                Description = model.Description
            };

            // calculating monthly contribution based on available budget and time left until the target date
            if (user!.Budget != null)
            {
                int totalMonths = ((model.TargetDate.Year - DateTime.Now.Year) * 12) + (model.TargetDate.Month - DateTime.Now.Month);
                int remainingDays = (model.TargetDate - DateTime.Now).Days;

                decimal maxMonthlyContribution = (totalMonths > 0) ? (user.Budget.TotalAmount / totalMonths) : (user.Budget.TotalAmount / (remainingDays / 30.0m));
                newGoal.MonthlyContribution = Math.Min(maxMonthlyContribution, model.TargetAmount / totalMonths);

                // updating the budget to reflect the goal's contribution
                user.Budget.TotalAmount -= newGoal.MonthlyContribution;

                context.Entry(user.Budget).State = EntityState.Modified;
            }

            context.Goals.Add(newGoal);
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
                    Description = goal.Description,
                    IsAchieved = goal.IsAchieved,
                    MonthlyGoalContribution = CalculateMonthlyGoalContribution(userId, goal)
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
                MonthlyContribution = goal.MonthlyContribution,
                IsAchieved = goal.IsAchieved
            };

            return model;
        }

        // soft delete the goal 
        public async Task DeleteGoalAsync(int goalId, string userId)
        {
            var goal = await context.Goals.Include(g => g.User).ThenInclude(u => u.Budget)
                                           .FirstOrDefaultAsync(g => g.Id == goalId && g.UserId == userId);

            if (goal != null && !goal.IsDeleted)
            {
                if (goal.IsAchieved)
                {
                    throw new InvalidOperationException("Cannot delete an achieved goal.");
                }

                // returning the contributed money to the budget
                goal!.User!.Budget.TotalAmount += goal.CurrentAmount;

                goal.IsDeleted = true;

                context.Goals.Update(goal);

                context.Entry(goal.User.Budget).State = EntityState.Modified;

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
        .ToListAsync();

            var goalViewModels = goals.Select(g => new GoalViewModel
            {
                Id = g.Id,
                TargetAmount = g.TargetAmount,
                CurrentAmount = g.CurrentAmount,
                TargetDate = g.TargetDate,
                //MonthlyContribution = g.MonthlyContribution,
                Description = g.Description,
                Currency = currency,
                IsAchieved = g.IsAchieved,
               MonthlyGoalContribution = CalculateMonthlyGoalContribution(userId, g)
            }).ToList();

            return goalViewModels;
        }
    }
}
