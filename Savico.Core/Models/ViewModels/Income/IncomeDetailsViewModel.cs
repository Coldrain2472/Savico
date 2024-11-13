namespace Savico.Core.Models.ViewModels.Income
{
    public class IncomeDetailsViewModel
    {
        public decimal Amount { get; set; }

        public string Source { get; set; } = null!;

        public DateTime Date { get; set; }
    }
}
