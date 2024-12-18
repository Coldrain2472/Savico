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

        public async Task<ExpenseInputViewModel> PrepareExpenseInputModelAsync(ExpenseInputViewModel inputModel, string userId)
        {
            var userCurrency = await context.Users
               .Where(u => u.Id == userId)
               .Select(u => u.Currency)
               .FirstOrDefaultAsync();

            var model = new ExpenseInputViewModel
            {
                Id = inputModel.Id,
                Amount = inputModel.Amount,
                Date = inputModel.Date,
                Description = inputModel.Description,
                Currency = userCurrency,
                Categories = await GetCategories()
            };

            return model;
        }

        public async Task AddExpenseAsync(ExpenseInputViewModel inputModel, string userId)
        {
            if (inputModel.Amount <= 0)
            {
                throw new ArgumentException("The amount must be greater than zero and a positive number.");
            }

            if (inputModel.CategoryId <= 0)
            {
                throw new ArgumentException("Please select a valid category.");
            }

            if (inputModel.Date == default || inputModel.Date.Year < 2023)
            {
                throw new ArgumentException("The date must be a valid date");
            }

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
                Date = inputModel.Date,
                CategoryId = inputModel.CategoryId
            };

            context.Expenses.Add(expense);

            await context.SaveChangesAsync();
        }

        public async Task<ExpenseInputViewModel> GetExpenseForEditAsync(int expenseId, string userId)
        {
            var expense = await context.Expenses
             .FirstOrDefaultAsync(e => e.Id == expenseId && e.UserId == userId);

            if (expense == null)
            {
                return null;
            }

            var userCurrency = await context.Users
              .Where(u => u.Id == userId)
              .Select(u => u.Currency)
              .FirstOrDefaultAsync();

            var model = new ExpenseInputViewModel
            {
                Id = expense.Id,
                Description = expense.Description,
                Amount = expense.Amount,
                Date = expense.Date,
                CategoryId = expense.CategoryId,
                Categories = await GetCategories(),
                Currency = userCurrency
            };

            return model;
        }

        public async Task<IEnumerable<ExpenseViewModel>> GetAllExpensesAsync(string userId)
        {
            var user = await context.Users.FindAsync(userId);

            var currency = user!.Currency;

            var expenses = await context.Expenses
               .Where(e => e.UserId == userId && !e.IsDeleted)
               .Select(e => new ExpenseViewModel
               {
                   Id = e.Id,
                   Description = e.Description,
                   Amount = e.Amount,
                   Date = e.Date,
                   CategoryName = e.Category!.Name,
                   Currency = currency!
               })
               .OrderBy(e=>e.Date)
               .ToListAsync();

            return expenses;
        }

        public async Task<ExpenseViewModel> GetExpenseByIdAsync(int expenseId, string userId)
        {
            var user = await context.Users.FindAsync(userId);
            var currency = user!.Currency;

            var expense = await context.Expenses
                .Where(e => e.Id == expenseId && e.UserId == userId)
                .Select(e => new ExpenseViewModel
                {
                    Id = e.Id,
                    Description = e.Description,
                    Amount = e.Amount,
                    Date = e.Date,
                    CategoryName = e.Category!.Name,
                    Currency = currency!
                })
                .FirstOrDefaultAsync();

            return expense;
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
            if (model.Amount <= 0)
            {
                throw new ArgumentException("The amount must be greater than zero and a positive number.");
            }

            if (model.CategoryId <= 0)
            {
                throw new ArgumentException("Please select a valid category.");
            }

            if (model.Date == default || model.Date.Year < 2023)
            {
                throw new ArgumentException("The date must be a valid date");
            }

            var expense = await context.Expenses
               .FirstOrDefaultAsync(e => e.Id == expenseId && e.UserId == userId);

            if (expense != null)
            {
                expense.Amount = model.Amount;
                expense.Description = model.Description;
                expense.Date = model.Date;
                expense.CategoryId = model.CategoryId;

                context.Expenses.Update(expense);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteExpenseAsync(int expenseId, string userId)
        {
            var expense = await context.Expenses.FindAsync(expenseId);

            if (expense != null && !expense.IsDeleted && expense.UserId == userId)
            {
                expense.IsDeleted = true;

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
            var categories = await context.Categories
                .Select(t => new CategoryViewModel
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToListAsync();

            return categories;
        }

        public async Task<IEnumerable<Expense>> GetExpensesForPeriodAsync(string userId, DateTime startDate, DateTime endDate)
        {
            var expenses = await context.Expenses
                .Where(e => e.UserId == userId && e.Date >= startDate && e.Date <= endDate && !e.IsDeleted)
                .ToListAsync();

            return expenses;
        }

        public async Task<List<CategoryExpenseViewModel>> GetExpenseCategories(string userId)
        {
            return await context.Expenses
                .Where(e => e.UserId == userId && !e.IsDeleted) 
                .GroupBy(e => e.Category) 
                .Select(g => new CategoryExpenseViewModel
                {
                    Name = g.Key.Name!,
                    TotalAmount = g.Sum(e => e.Amount)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ExpenseViewModel>> GetFilteredExpensesAsync(string userId, string filterBy)
        {
            var query = context.Expenses
                .Where(e => e.UserId == userId && !e.IsDeleted);

            switch (filterBy?.ToLower())
            {
                case "recent":
                    query = query.OrderByDescending(e => e.Date);
                    break;
                case "amount":
                    query = query.OrderByDescending(e => e.Amount);
                    break;
                default:
                    query = query.OrderBy(e => e.Date);
                    break;
            }

            var result = await query
                 .Select(e => new ExpenseViewModel
                 {
                     Id = e.Id,
                     Amount = e.Amount,
                     Description = e.Description,
                     Date = e.Date,
                     Currency = e.User!.Currency,
                     CategoryName = e.Category!.Name
                 })
                 .ToListAsync();

            return result;
        }

        public async Task<(IEnumerable<ExpenseViewModel> Expenses, int TotalItems)> GetPaginatedExpensesAsync(string userId, int pageNumber, int pageSize, string filterOption = "")
        {
            var query = context.Expenses
                .Where(e => e.UserId == userId && !e.IsDeleted)
                .AsQueryable();

            switch (filterOption)
            {
                case "recent":
                    query = query.OrderByDescending(e => e.Date);  
                    break;
                case "amount":
                    query = query.OrderByDescending(e => e.Amount); 
                    break;
                case "reset": 
                default:
                    query = query.OrderBy(e => e.Date); 
                    break;
            }

            var totalItems = await query.CountAsync();

            var expenses = await query
                .Skip((pageNumber - 1) * pageSize) 
                .Take(pageSize)
                .Select(e => new ExpenseViewModel
                {
                    Id = e.Id,
                    Amount = e.Amount,
                    Date = e.Date,
                    CategoryName = e.Category!.Name,
                    Description = e.Description,
                    Currency = e.User!.Currency
                })
                .ToListAsync();

            return (expenses, totalItems);
        }
    }
}