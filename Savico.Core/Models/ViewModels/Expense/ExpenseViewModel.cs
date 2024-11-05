namespace Savico.Core.Models.ViewModels.Expense
{
    using Savico.Core.Models.ViewModels.Category;
    using static Savico.Infrastructure.Data.Constants.DataConstants;
    using System.ComponentModel.DataAnnotations;

    public class ExpenseViewModel
    {
        public int Id { get; set; }

        //[Required]
        //[Range(0.00, double.MaxValue, ErrorMessage = RangeErrorMessage)]
        public decimal Amount { get; set; }

        //[Required]
        public DateTime Date { get; set; }

        public string? Description { get; set; }

        //[Required]
        public int CategoryId { get; set; }

        public ICollection<CategoryViewModel> Categories { get; set; } = new HashSet<CategoryViewModel>();

        public string FormattedDate => Date.ToString("dd MMM yyyy");

        public string? CategoryName { get; set; }
    }
}
