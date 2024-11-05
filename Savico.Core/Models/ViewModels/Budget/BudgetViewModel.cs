namespace Savico.Models.ViewModels.Budget
{
	using System.ComponentModel.DataAnnotations;
	using static Savico.Infrastructure.Data.Constants.DataConstants;

	public class BudgetViewModel
	{
		public int Id { get; set; }

		[Required]
		[Display(Name = "Total Amount")]
		[Range(0.00, double.MaxValue, ErrorMessage = RangeErrorMessage)]
		public decimal TotalAmount { get; set; }

		[Required]
		public DateTime StartDate { get; set; }

		[Required]
		public DateTime EndDate { get; set; }
	}
}
