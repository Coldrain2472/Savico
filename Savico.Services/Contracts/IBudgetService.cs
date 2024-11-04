namespace Savico.Services.Contracts
{
	using Savico.Core.Models;
    using Savico.Models.ViewModels.Budget;

    public interface IBudgetService
	{
		Task<IEnumerable<BudgetViewModel>> GetAllBudgetsAsync(); // retrieve budget data

		Task<Budget> GetBudgetByIdAsync(int id); // retrieve budget data

		Task AddBudgetAsync(Budget budget); // manage budget creation

		Task UpdateBudgetAsync(Budget budget); // manage budget edits

		Task DeleteBudgetAsync(int id); // deletes a budget by its Id

		Task<decimal?> CalculateRemainingBudgetAsync(int budgetId); // calculates the remaining amount in a budget after expenses
	}
}
