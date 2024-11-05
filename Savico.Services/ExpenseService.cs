namespace Savico.Services
{
    using Microsoft.EntityFrameworkCore;
    using Savico.Core.Models;
    using Savico.Core.Models.ViewModels.Category;
    using Savico.Core.Models.ViewModels.Expense;
    using Savico.Infrastructure;
    using Savico.Services.Contracts;

    public class ExpenseService : IExpenseService
    {
        private readonly SavicoDbContext context;

        public ExpenseService(SavicoDbContext context)
        {
            this.context = context;
        }

        public async Task<ExpenseInputViewModel> PrepareExpenseInputModelAsync()
        {
            var model = new ExpenseInputViewModel
            {
                Categories = await GetCategories() // loading the categories
            };

            return model;
        }

        public async Task AddExpenseAsync(ExpenseInputViewModel model, string userId)
        {
            var expense = new Expense
            {
                Description = model.Description,
                Amount = model.Amount,
                Date = model.Date,
                CategoryId = model.CategoryId,
                UserId = userId
            };

            await context.Expenses.AddAsync(expense);
            await context.SaveChangesAsync();
        }

        public async Task EditExpenseAsync(int expenseId, ExpenseInputViewModel model, string userId)
        {
            var expense = await context.Expenses
                .FirstOrDefaultAsync(e => e.Id == expenseId && e.UserId == userId);

            if (expense != null)
            {
                expense.Description = model.Description;
                expense.Amount = model.Amount;
                expense.Date = model.Date;
                expense.CategoryId = model.CategoryId;

                context.Expenses.Update(expense);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ExpenseViewModel>> GetAllExpensesAsync(string userId)
        {
            return await context.Expenses
                .Where(e => e.UserId == userId)
                .Select(e => new ExpenseViewModel
                {
                    Id = e.Id,
                    Description = e.Description,
                    Amount = e.Amount,
                    Date = e.Date
                })
                .ToListAsync();
        }

        public async Task<ExpenseViewModel> GetExpenseByIdAsync(int expenseId, string userId)
        {
            var expense = await context.Expenses
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.Id == expenseId && e.UserId == userId);

            if (expense == null)
            {
                return null;
            }

            return new ExpenseViewModel
            {
                Id = expense.Id,
                Description = expense.Description,
                Amount = expense.Amount,
                Date = expense.Date,
                CategoryId = expense.CategoryId
            };
        }

        public async Task<string> GetCategoryNameByIdAsync(int categoryId)
        {
            var category = await context.Categories
                .Where(c => c.Id == categoryId)
                .Select(c => c.Name)
                .FirstOrDefaultAsync();

            return category ?? string.Empty; 
        }

        public async Task DeleteExpenseAsync(int expenseId, string userId)
        {
            var expense = await context.Expenses
                .FirstOrDefaultAsync(e => e.Id == expenseId && e.UserId == userId);

            if (expense != null)
            {
                context.Expenses.Remove(expense);
                await context.SaveChangesAsync();
            }
        }

        public async Task<decimal?> CalculateRemainingBudgetAsync(string userId)
        {
            var totalIncome = await context.Incomes
                .Where(i => i.UserId == userId)
                .SumAsync(i => (decimal?)i.Amount) ?? 0;

            var totalExpenses = await context.Expenses
                .Where(e => e.UserId == userId)
                .SumAsync(e => (decimal?)e.Amount) ?? 0;

            return totalIncome - totalExpenses;
        }

        public async Task<ICollection<CategoryViewModel>> GetCategories()
        {
            return await context.Categories
                .Select(t => new CategoryViewModel()
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToListAsync();
        }
    }
}
