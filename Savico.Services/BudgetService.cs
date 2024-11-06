namespace Savico.Services
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
            // returns the budget (the money that we have after all the expenses)
        {
            var user = await context.Users
                .Include(u => u.Budget)
                .Include(u => u.Incomes)
                .Include(u => u.Expenses)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return null;
            }

            var totalIncome = user.Incomes?.Sum(i => i.Amount); // GetTotalIncomeAsync(userId);
            var totalExpense = user.Expenses?.Sum(e => e.Amount); // GetTotalExpenseAsync(userId);

            return totalIncome - totalExpense;
        }

        public async Task<decimal> GetTotalIncomeAsync(string userId) // returns the sum of the income
        {
            var income = await context.Incomes
                .Where(i => i.UserId == userId)
                .SumAsync(i => i.Amount);

            return income;
        }

        public async Task<decimal> GetTotalExpenseAsync(string userId) // return the sum of the expenses
        {
            var expenses = await context.Expenses
                .Where(e => e.UserId == userId)
                .SumAsync(e => e.Amount);

            return expenses;
        }
    }
}
