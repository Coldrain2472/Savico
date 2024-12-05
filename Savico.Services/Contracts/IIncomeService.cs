namespace Savico.Services.Contracts
{
    using Savico.Core.Models;
    using Savico.Core.Models.ViewModels.Income;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IIncomeService
    {
       Task AddIncomeAsync(IncomeInputViewModel model, string userId); // adds an income

        Task DeleteIncomeAsync(int incomeId, string userId);  // deletes an income

        Task<IEnumerable<IncomeViewModel>> GetAllIncomesAsync(string userId); // retrieves all incomes

        Task<IncomeViewModel> GetIncomeByIdAsync(int incomeId, string userId); // retrieve an income by a specific id

        Task UpdateIncomeAsync(int incomeId, IncomeInputViewModel model, string userId); // updates an income

        Task<IncomeInputViewModel> PrepareIncomeInputModelAsync(string userId); // prepares the input model for adding/editing income

        Task<IncomeInputViewModel> GetIncomeForEditAsync(int id, string userId); // prepares the income for edit

        Task<IEnumerable<Income>> GetIncomesForPeriodAsync(string userId, DateTime startDate, DateTime endDate);
        // gets all the incomes for a specific period for the Report service
    }
}
