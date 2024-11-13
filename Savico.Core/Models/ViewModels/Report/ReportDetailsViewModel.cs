namespace Savico.Core.Models.ViewModels.Report
{
    using Savico.Core.Models.ViewModels.Expense;
    using Savico.Core.Models.ViewModels.Income;

    public class ReportDetailsViewModel
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal TotalIncome { get; set; }

        public decimal TotalExpense { get; set; }

        public string? Currency {  get; set; }

        public ICollection<IncomeDetailsViewModel> Incomes { get; set; } = new List<IncomeDetailsViewModel>();

        public ICollection<ExpenseDetailsViewModel> Expenses { get; set; } = new List<ExpenseDetailsViewModel>();
    }
}
