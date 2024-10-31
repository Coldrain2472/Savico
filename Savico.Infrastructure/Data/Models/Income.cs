namespace Savico.Core.Models
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Savico.Infrastructure.Data.Contracts;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Savico.Infrastructure.Data.Constants.DataConstants;
    using static Savico.Infrastructure.Data.Constants.DataConstants.IncomeConstants;

    public class Income : ISoftDeletable
    {
        [Key]
        [Comment("Income identifier")]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;

        [Required]
        [MaxLength(SourceMaxLength)]
        [Comment("Income's source")]
        // salary, freelance, etc...
        public string Source { get; set; } = null!;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = RangeErrorMessage)]
        [Comment("Income's amount")]
        public decimal Amount { get; set; }

        [Required]
        [Comment("Date of receiving the income")]
        public DateTime Date { get; set; }

        [Comment("Indicates if the income is soft-deleted")]
        public bool IsDeleted { get; set; } = false;
    }
}
