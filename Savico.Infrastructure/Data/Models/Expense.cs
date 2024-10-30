namespace Savico.Core.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Expense
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid BudgetId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [StringLength(100)]
        public string Category { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }
}
