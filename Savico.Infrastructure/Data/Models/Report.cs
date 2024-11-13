namespace Savico.Infrastructure.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using Savico.Core.Models;

    public class Report
    {
        [Comment("Report identifier")]
        public int Id { get; set; }

        [Required]
        [Comment("User who created the report")]
        public string? UserId { get; set; }

        public User? User { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Comment("Start date of the report period")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
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
