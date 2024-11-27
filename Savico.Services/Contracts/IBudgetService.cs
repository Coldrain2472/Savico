namespace Savico.Services.Contracts
{
    using System.Threading.Tasks;

    public interface IBudgetService
    {
        Task<decimal?> CalculateRemainingBudgetAsync(string userId); // calculates the budget after the expenses

        Task<decimal> GetTotalIncomeAsync(string userId); // calculates the sum of the incomes

        Task<decimal> GetTotalExpenseAsync(string userId); // calculates the sum of the expenses
    }
}
