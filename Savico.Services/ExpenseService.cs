﻿namespace Savico.Services
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

        public async Task AddExpenseAsync(ExpenseInputViewModel inputModel, string userId)
        {
            var budget = await context.Budgets
               .FirstOrDefaultAsync(b => b.UserId == userId);

            if (budget == null)
            {
                budget = new Budget
                {
                    UserId = userId,
                    TotalAmount = 0
                };
                context.Budgets.Add(budget);
                await context.SaveChangesAsync();
            }

            var expense = new Expense
            {
                Amount = inputModel.Amount,
                Description = inputModel.Description,
                BudgetId = budget.Id,
                UserId = userId,
                CategoryId = inputModel.CategoryId
            };

            context.Expenses.Add(expense);

            await context.SaveChangesAsync();
        }

        public async Task<ExpenseInputViewModel> GetExpenseForEditAsync(int expenseId, string userId)
        {
            var expense = await context.Expenses
        .Where(e => e.Id == expenseId && e.UserId == userId)
        .FirstOrDefaultAsync();

            if (expense == null)
            {
                return null; 
            }

            var categories = await context.Categories
          .Select(c => new CategoryViewModel
          {
              Id = c.Id,
              Name = c.Name
          })
          .ToListAsync();

            return new ExpenseInputViewModel
            {
                Id = expense.Id,
                Description = expense.Description,
                Amount = expense.Amount,
                Date = expense.Date,
                CategoryId = expense.CategoryId,
                Categories = categories 
            };
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

        public async Task UpdateExpenseAsync(int expenseId, ExpenseInputViewModel model, string userId)
        {
            var expense = await context.Expenses
                .FirstOrDefaultAsync(e => e.Id == expenseId && e.UserId == userId);

            expense.Description = model.Description;
            expense.Amount = model.Amount;
            expense.Date = model.Date;
            expense.CategoryId = model.CategoryId;

            context.Expenses.Update(expense);
            await context.SaveChangesAsync();
        }

        public async Task DeleteExpenseAsync(int expenseId, string userId)
        {
            var expense = await context.Expenses
                .FirstOrDefaultAsync(e => e.Id == expenseId && e.UserId == userId);

            if (expense != null && expense.IsDeleted == false) // trying to implement soft delete?
            {
                expense.IsDeleted = true;
               // context.Expenses.Remove(expense);
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
