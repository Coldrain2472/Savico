namespace Savico.Services
{
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
	using Savico.Core.Models;
	using Savico.Core.Models.ViewModels.Category;
	using Savico.Core.Models.ViewModels.Expense;
	using Savico.Core.Models.ViewModels.Income;
	using Savico.Infrastructure;
	using Savico.Services.Contracts;
	using System.Security.Claims;


	public class ExpenseService : IExpenseService
	{
		private readonly SavicoDbContext context;
		private readonly UserManager<User> userManager;

		public ExpenseService(SavicoDbContext context, UserManager<User> userManager)
		{
			this.context = context;
			this.userManager = userManager;
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
			var user = await userManager.FindByIdAsync(userId);

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
			 .ToListAsync();

			return expenses;
		}

		public async Task<ExpenseViewModel> GetExpenseByIdAsync(int expenseId, string userId)
		{
			var user = await userManager.FindByIdAsync(userId);
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
