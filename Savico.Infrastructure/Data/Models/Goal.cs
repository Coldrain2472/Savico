namespace Savico.Core.Models
{
    using Microsoft.EntityFrameworkCore;
    using Savico.Infrastructure.Data.Contracts;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Savico.Infrastructure.Data.Constants.DataConstants;

    public class Goal : ISoftDeletable
    {
        [Key]
        [Comment("Goal identifier")]
        public int Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

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
