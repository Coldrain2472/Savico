namespace Savico.Core.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Goal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Target amount must be greater than zero.")]
        public decimal TargetAmount { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Current amount must be greater than or equal to zero.")]
        public decimal CurrentAmount { get; set; }

        [Required]
        public DateTime TargetDate { get; set; }
    }
}
