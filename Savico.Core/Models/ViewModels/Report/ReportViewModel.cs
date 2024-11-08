namespace Savico.Core.Models.ViewModels.Report
{
    public class ReportViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }

        public List<IncomeViewModel> Incomes { get; set; } = new List<IncomeViewModel>(); // add such collection in the entity?
        public List<ExpenseViewModel> Expenses { get; set; } = new List<ExpenseViewModel>(); // add such collection in the entity?
    }

    // Supporting ViewModels for displaying incomes and expenses
    public class IncomeViewModel
    {
        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public string? Source { get; set; }
    }

    public class ExpenseViewModel
    {
        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public string? Description { get; set; }
    }

}
