namespace Savico.Core.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Income
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Source { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
