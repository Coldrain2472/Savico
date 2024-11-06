namespace Savico.Services.Contracts
{
    using Savico.Core.Models.ViewModels.Category;
    using Savico.Core.Models.ViewModels.Expense;

    public interface IExpenseService
    {
        Task<IEnumerable<ExpenseViewModel>> GetAllExpensesAsync(string userId); // retrieves all expenses

        Task AddExpenseAsync(ExpenseInputViewModel model, string userId); // adds an expense

        Task DeleteExpenseAsync(int expenseId, string userId);  // deletes an expense

        Task<ExpenseViewModel> GetExpenseByIdAsync(int expenseId, string userId); // retrieve an expense by a specific id

        Task EditExpenseAsync(int expenseId, ExpenseInputViewModel model, string userId); // edits an expense

        Task<decimal?> CalculateRemainingBudgetAsync(string userId); // calculates the budget

        Task<ExpenseInputViewModel> PrepareExpenseInputModelAsync(); // prepares the input model for edit/add

        Task<string> GetCategoryNameByIdAsync(int categoryId); // retrieves a category name by id

        Task<ICollection<CategoryViewModel>> GetCategories(); // retrieves the categories
    }
}
