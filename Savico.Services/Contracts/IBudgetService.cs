namespace Savico.Services.Contracts
{
    using System.Threading.Tasks;

    public interface IBudgetService
	{
        Task<decimal?> CalculateRemainingBudgetAsync(string userId);

        Task<decimal> GetTotalIncomeAsync(string userId);

        Task<decimal> GetTotalExpenseAsync(string userId);
    }
}
