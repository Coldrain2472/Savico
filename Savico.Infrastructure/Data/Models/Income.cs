namespace Savico.Core.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Savico.Infrastructure.Data.Constants.DataConstants;
    using static Savico.Infrastructure.Data.Constants.DataConstants.IncomeConstants;

    public class Income
    {
        [Comment("Income identifier")]
        public int Id { get; set; }

        [Required]
        public string? UserId { get; set; }

        public User? User { get; set; } 

        [Required]
        [MaxLength(SourceMaxLength)]
        [Comment("Income's source")]
        // salary, freelance, etc...
        public string? Source { get; set; }

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
