﻿namespace Savico.Services
{
    using Microsoft.EntityFrameworkCore;
    using Savico.Infrastructure;
    using Savico.Services.Contracts;
    using System.Linq;
    using System.Threading.Tasks;

    public class BudgetService : IBudgetService
    {
        private readonly SavicoDbContext context;

        public BudgetService(SavicoDbContext context)
        {
            this.context = context;
        }

        public async Task<decimal?> CalculateRemainingBudgetAsync(string userId)
        // returns the budget (the money that we have after all the expenses and the contribution to the goal)
        {
            var user = await context.Users
                .Include(u => u.Budget)
                .Include(u => u.Incomes)
                .Include(u => u.Expenses)
                .Include(u => u.Goals)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return null;
            }

            var totalIncome = user.Incomes?.Where(i => !i.IsDeleted).Sum(i => i.Amount); // GetTotalIncomeAsync(userId);

            var totalExpense = user.Expenses?.Where(e => !e.IsDeleted).Sum(e => e.Amount); // GetTotalExpenseAsync(userId);

            decimal totalGoalContribution = 0;

            foreach (var goal in user.Goals.Where(g => !g.IsDeleted))
            {
                // calculates the number of months remaining to reach the goal
                var monthsRemaining = (goal.TargetDate - DateTime.Now).Days / 30; // approximation

                if (monthsRemaining > 0)
                {
                    // adding the monghtly contribution to the totalgoalcontribution
                    var monthlyContribution = (goal.TargetAmount - goal.CurrentAmount) / monthsRemaining;
                    totalGoalContribution += monthlyContribution;
                }
            }

            var remainingBudget = totalIncome - totalExpense - totalGoalContribution;

            return remainingBudget;
        }

        public async Task<decimal> GetTotalIncomeAsync(string userId) // returns the sum of the income
        {
            var income = await context.Incomes
                .Where(i => i.UserId == userId && !i.IsDeleted)
                .SumAsync(i => i.Amount);

            return income;
        }

        public async Task<decimal> GetTotalExpenseAsync(string userId) // return the sum of the expenses
        {
            var expenses = await context.Expenses
                .Where(e => e.UserId == userId && !e.IsDeleted)
                .SumAsync(e => e.Amount);

            return expenses;
        }
    }
}
