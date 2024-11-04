namespace Savico.Models.ViewModels.Budget
{
	using System.ComponentModel.DataAnnotations;

	public class BudgetViewModel
	{
		public int Id { get; set; }

		[Required]
		[Display(Name = "Total Amount")]
		public decimal TotalAmount { get; set; }

		[Required]
		public DateTime StartDate { get; set; }

		[Required]
		public DateTime EndDate { get; set; }
	}
}
