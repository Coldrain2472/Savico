namespace Savico.Core.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using static Savico.Infrastructure.Data.Constants.DataConstants;

    public class Goal
    {
        [Comment("Goal identifier")]
        public int Id { get; set; }

        [Required]
        public string? UserId { get; set; } 

        public User? User { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = RangeErrorMessage)]
        [Comment("Goal's target amount")]
        public decimal TargetAmount { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = RangeErrorMessage)]
        // "Current amount must be greater than or equal to zero."
        [Comment("User's current amount towards goal")]
        public decimal CurrentAmount { get; set; }

        [Required]
        [Comment("Target date for reaching the goal")]
        public DateTime TargetDate { get; set; }

        [Comment("Indicates if the goal is soft-deleted")]
        public bool IsDeleted { get; set; } = false;
    }
}
