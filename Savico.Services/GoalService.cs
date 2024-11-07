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

        // Add a goal
        public async Task AddGoalAsync(GoalInputViewModel model, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            var goal = new Goal
            {
                UserId = userId,
                TargetAmount = model.TargetAmount,
                CurrentAmount = model.CurrentAmount,
                TargetDate = model.TargetDate,
                IsDeleted = false
            };

            await context.Goals.AddAsync(goal);
            await context.SaveChangesAsync();
        }

        // Get a goal by ID for a user
        public async Task<GoalViewModel> GetGoalByIdAsync(int goalId, string userId)
        {
            var goal = await context.Goals
                .Where(g => g.UserId == userId && g.Id == goalId && !g.IsDeleted)
                .FirstOrDefaultAsync();

            if (goal != null)
            {
                return new GoalViewModel
                {
                    Id = goal.Id,
                    TargetAmount = goal.TargetAmount,
                    CurrentAmount = goal.CurrentAmount,
                    TargetDate = goal.TargetDate
                };
            }

            return null;
        }

        // Calculate the amount the user needs to save each month
        public decimal CalculateMonthlySavings(decimal targetAmount, decimal currentAmount, DateTime targetDate)
        {
            var monthsRemaining = (targetDate.Year - DateTime.Now.Year) * 12 + targetDate.Month - DateTime.Now.Month;

            if (monthsRemaining <= 0)
            {
                return 0; // If the target date is today or in the past, no monthly saving required
            }

            var requiredAmount = targetAmount - currentAmount;

            return requiredAmount / monthsRemaining;
        }

        // Soft delete the goal (mark as deleted)
        public async Task DeleteGoalAsync(int goalId, string userId)
        {
            var goal = await context.Goals
                .FirstOrDefaultAsync(g => g.UserId == userId && g.Id == goalId && !g.IsDeleted);

            if (goal != null)
            {
                goal.IsDeleted = true;
                await context.SaveChangesAsync();
            }
        }

        // Get all active goals for the user
        public async Task<IEnumerable<GoalViewModel>> GetAllGoalsAsync(string userId)
        {
            var goals = await context.Goals
                .Where(g => g.UserId == userId && !g.IsDeleted)
                .Select(g => new GoalViewModel
                {
                    Id = g.Id,
                    TargetAmount = g.TargetAmount,
                    CurrentAmount = g.CurrentAmount,
                    TargetDate = g.TargetDate
                })
                .ToListAsync();

            return goals;
        }
    }
}
