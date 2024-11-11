namespace Savico.Core.Models.ViewModels.Home
{
    public class HomeViewModel
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? Currency { get; set; }

        public decimal TotalIncome { get; set; }

        public decimal TotalExpense { get; set; }

        public decimal Budget { get; set; }
    }
}
