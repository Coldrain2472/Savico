﻿namespace Savico.Services
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

        public async Task<GoalInputViewModel> GetGoalInputViewModelAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            var model = new GoalInputViewModel()
            {
                Currency = user?.Currency
            };

            return model;
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
                goal.CurrentAmount = model.CurrentAmount; // ?
                goal.TargetAmount = model.TargetAmount;

                // mark as achieved if the current amount meets or exceeds the target amount
                if (goal.CurrentAmount >= goal.TargetAmount)
                {
                    goal.IsAchieved = true;
                }

                context.Goals.Update(goal);
                await context.SaveChangesAsync();
            }
        } 

        
        public async Task<GoalViewModel> GetGoalByIdAsync(int goalId, string userId)
        {
            var goal = await context.Goals
                .FirstOrDefaultAsync(g => g.UserId == userId && g.Id == goalId && !g.IsDeleted);

            var userCurrency = await context.Users
                .Where(u => u.Id == userId)
                .Select(u => u.Currency)
                .FirstOrDefaultAsync();

            if (goal is null && goal!.UserId != userId) // goal == null
            {
                return null;
            }

            var goalModel = new GoalViewModel
            {
                Id = goal.Id,
                TargetAmount = goal.TargetAmount,
                CurrentAmount = goal.CurrentAmount,
                TargetDate = goal.TargetDate,
                Currency = userCurrency,
                Description = goal.Description,
                IsAchieved = goal.IsAchieved
            };

            return goalModel;
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
              //  MonthlyContribution = goal.MonthlyContribution,
                IsAchieved = goal.IsAchieved
            };

            return model;
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
                Description = g.Description,
                Currency = currency,
                IsAchieved = g.IsAchieved
            })
                .ToList();

            return goalViewModels;
        }
        
        public async Task AddGoalAsync(GoalInputViewModel model, string userId)
        {
            var user = await userManager.Users
                .Include(u => u.Budget)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var newGoal = new Goal
            {
                UserId = userId,
                TargetAmount = model.TargetAmount,
                CurrentAmount = 0,
                TargetDate = model.TargetDate,
                Description = model.Description,
                IsAchieved = model.IsAchieved
            };

            await context.Goals.AddAsync(newGoal);
            await context.SaveChangesAsync();
        } 

        public async Task ContributeToGoalAsync(GoalContributeViewModel model, string userId)
        {
            var user = await context.Users
       .Include(u => u.Budget) 
       .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            if (user.Budget == null)
            {
                throw new InvalidOperationException("User does not have a budget.");
            }

            var goal = await context.Goals
                 .FirstOrDefaultAsync(g => g.Id == model.GoalId && g.UserId == userId);

            if (goal == null)
            {
                throw new InvalidOperationException("Goal not found or does not belong to the user.");
            }


            if (model.ContributionAmount <= 0)
            {
                throw new InvalidOperationException("Contribution must be greater than zero.");
            }


            if (model.ContributionAmount > user.Budget.TotalAmount) // TO DO: fix functionality
            {
                throw new InvalidOperationException("Insufficient budget for contribution.");
            }


            // Update goal's current amount
            goal.CurrentAmount += model.ContributionAmount;

            // Deduct the contribution from the user's budget
            user.Budget.TotalAmount -= model.ContributionAmount;
            context.Update(goal);
            context.Update(user);

            await context.SaveChangesAsync();
        } 

        public async Task DeleteGoalAsync(int goalId, string userId)
        {
            var goal = await context.Goals
                 .Include(g => g.User)
                 .ThenInclude(u => u.Budget)
                 .FirstOrDefaultAsync(g => g.Id == goalId && g.UserId == userId);

            if (goal != null && !goal.IsDeleted)
            {
                if (goal.IsAchieved)
                {
                    throw new InvalidOperationException("Cannot delete an achieved goal.");
                }

                goal.User!.Budget.TotalAmount += goal.CurrentAmount;

                goal.IsDeleted = true;

                context.Goals.Update(goal);
                context.Entry(goal.User.Budget).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        } 

        public async Task<GoalContributeViewModel> GetGoalContributeViewModelAsync(int goalId, string userId)
        {
            var goal = await context.Goals
                .Where(g => g.Id == goalId && g.UserId == userId)
                .FirstOrDefaultAsync();
            var user = await userManager.FindByIdAsync(userId);

            var currency = user!.Currency;

            if (goal == null)
            {
                return null;
            }

            return new GoalContributeViewModel
            {
                GoalId = goal.Id,
                Currency = currency
            };
        } 

    }
}
