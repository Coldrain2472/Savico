namespace Savico.Infrastructure.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using Savico.Infrastructure.Data.Contracts;
    using Savico.Core.Models;

    public class Report : ISoftDeletable
    {
        [Key]
        [Comment("Report identifier")]
        public int Id { get; set; }

        [Required]
        [Comment("User who created the report")]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        [Required]
        [Comment("Start date of the report period")]
        public DateTime StartDate { get; set; }

        [Required]
        [Comment("End date of the report period")]
        public DateTime EndDate { get; set; }

        [Required]
        [Comment("Total income during the period")]
        public decimal TotalIncome { get; set; }

        [Required]
        [Comment("Total expense during the period")]
        public decimal TotalExpense { get; set; }

        [Comment("Indicates if the report is soft-deleted")]
        public bool IsDeleted { get; set; } = false;
    }
}
