namespace Savico.Core.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Savico.Infrastructure.Data.Constants.DataConstants;

    public class Budget
    {
        [Key]
        [Comment("Budget identifier")]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

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

        public ICollection<Expense> Expenses { get; set; } = new HashSet<Expense>();
    }
}
