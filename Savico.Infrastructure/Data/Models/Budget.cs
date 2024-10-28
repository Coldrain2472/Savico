using System.ComponentModel.DataAnnotations;

namespace Savico.Core.Models
{
    public class Budget
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

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
