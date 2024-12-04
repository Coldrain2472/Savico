namespace Savico.Services.Contracts
{
    using Savico.Core.Models;
    using Savico.Core.Models.ViewModels.Category;
    using Savico.Core.Models.ViewModels.Expense;

    public interface IExpenseService
    {
        Task<IEnumerable<ExpenseViewModel>> GetAllExpensesAsync(string userId); // retrieves all expenses

        Task AddExpenseAsync(ExpenseInputViewModel model, string userId); // adds an expense

        Task DeleteExpenseAsync(int expenseId, string userId);  // deletes an expense

        Task<ExpenseViewModel> GetExpenseByIdAsync(int expenseId, string userId); // retrieve an expense by a specific id

        Task<decimal?> CalculateRemainingBudgetAsync(string userId); // calculates the budget

        Task<ExpenseInputViewModel> PrepareExpenseInputModelAsync(ExpenseInputViewModel inputModel, string userId); 
        // prepares the input model for edit/add

        Task<string> GetCategoryNameByIdAsync(int categoryId); // retrieves a category name by id

        Task<ICollection<CategoryViewModel>> GetCategories(); // retrieves the categories

        Task<ExpenseInputViewModel> GetExpenseForEditAsync(int expenseId, string userId); // retrieves the expense for edit

        Task UpdateExpenseAsync(int expenseId, ExpenseInputViewModel model, string userId); // updates and expense

        Task<IEnumerable<Expense>> GetExpensesForPeriodAsync(string userId, DateTime startDate, DateTime endDate);
        // get expenses for a specific period for the Report service

        Task<List<CategoryExpenseViewModel>> GetExpenseCategories(string userId); // shows expenses by category names (used it for the chart on home index page)

        Task<IEnumerable<ExpenseViewModel>> GetFilteredExpensesAsync(string userId, string filterBy); // will use this method for the expense filtering function

        Task<(IEnumerable<ExpenseViewModel> Expenses, int TotalItems)> GetPaginatedExpensesAsync(string userId, int pageNumber, int pageSize, string filterOption = "");
        // will use this method for the pagination function
    }
}
