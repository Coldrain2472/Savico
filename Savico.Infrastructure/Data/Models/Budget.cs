namespace Savico.Core.Models
{
    using Microsoft.EntityFrameworkCore;
    using Savico.Infrastructure.Data.Contracts;
    using Savico.Infrastructure.Data.Models;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Savico.Infrastructure.Data.Constants.DataConstants;

    public class Budget : ISoftDeletable
    {
        [Key]
        [Comment("Budget identifier")]
        public int Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        [Required]
        [Comment("Budget's start date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Comment("Budget's end date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = RangeErrorMessage)]
        [Comment("Budget's total amount")]
        public decimal TotalAmount { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public BudgetCategory Category { get; set; } = null!;

        [Comment("Indicates if the budget is soft-deleted")]
        public bool IsDeleted { get; set; } = false;

        public ICollection<Expense> Expenses { get; set; } = new HashSet<Expense>();
    }
}
