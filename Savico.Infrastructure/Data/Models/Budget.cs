namespace Savico.Core.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Budget
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total amount must be greater than zero.")]
        public decimal TotalAmount { get; set; }

        [StringLength(100)]
        public string CategoryBudget { get; set; }
    }
}
