namespace Savico.Core.Models.ViewModels.Report
{
    public class ReportViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal TotalIncome { get; set; }

        public decimal TotalExpense { get; set; }

        public string? Currency { get; set; }
    }
}
