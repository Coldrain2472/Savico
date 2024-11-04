namespace Savico.Services
{
	using Savico.Core.Models;
	using Savico.Infrastructure.Common;
	using Savico.Services.Contracts;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public class BudgetService : IBudgetService
	{
		private readonly IRepository<Budget, int> budgetRepository;
		private readonly IRepository<Expense, int> expenseRepository;

		public BudgetService(IRepository<Budget, int> budgetRepository, IRepository<Expense, int> expenseRepository)
		{
			this.budgetRepository = budgetRepository;
			this.expenseRepository = expenseRepository;
		}

		public async Task<IEnumerable<Budget>> GetAllBudgetsAsync()
		{
			return await budgetRepository.GetAllAsync();
		}

		public async Task<Budget> GetBudgetByIdAsync(int id)
		{
			return await budgetRepository.GetByIdAsync(id);
		}

		public async Task AddBudgetAsync(Budget budget)
		{
			await budgetRepository.AddAsync(budget);
		}

		public async Task UpdateBudgetAsync(Budget budget)
		{
			await budgetRepository.UpdateAsync(budget);
		}

		public async Task DeleteBudgetAsync(int id)
		{
			var budget = await budgetRepository.GetByIdAsync(id);

			if (budget != null)
			{
				await budgetRepository.DeleteAsync(budget);
			}
		}

		public async Task<decimal?> CalculateRemainingBudgetAsync(int budgetId)
		{
			var budget = await budgetRepository.GetByIdAsync(budgetId);

			if (budget == null)
			{
				return null;
			}

			var expenses = await expenseRepository.FindAsync(e => e.BudgetId == budgetId);
			var totalExpenses = expenses.Sum(e => e.Amount);

			return budget.TotalAmount - totalExpenses;
		}
	}
}
