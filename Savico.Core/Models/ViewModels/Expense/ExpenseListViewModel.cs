namespace Savico.Core.Models.ViewModels.Expense
{
    public class ExpenseListViewModel
    {
        public IEnumerable<ExpenseViewModel> Expenses { get; set; } = new List<ExpenseViewModel>();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize); 
    }
}
