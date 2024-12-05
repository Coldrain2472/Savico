namespace Savico.Core.Models.ViewModels.Expense
{
    using Savico.Core.Models.ViewModels.Category;
    using System.ComponentModel.DataAnnotations;
    using static Savico.Infrastructure.Data.Constants.DataConstants;

    public class ExpenseInputViewModel
    {
        public int Id { get; set; }

        [Required]
        [Range(0.00, double.MaxValue, ErrorMessage = RangeErrorMessage)]
        public decimal Amount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "You must choose a category")]
        public int CategoryId { get; set; }

        public ICollection<CategoryViewModel> Categories { get; set; } = new HashSet<CategoryViewModel>();

        public int BudgetId {  get; set; }

        public string? Currency {  get; set; }
    }
}
