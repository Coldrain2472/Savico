namespace Savico.Core.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using static Savico.Infrastructure.Data.Constants.DataConstants;

    public class Budget 
    {
        [Comment("Budget identifier")]
        public int Id { get; set; }

        [Required]
        public string? UserId { get; set; } 

        public User? User { get; set; } 

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

        [Comment("Indicates if the budget is soft-deleted")]
        public bool IsDeleted { get; set; } = false;

        public ICollection<Expense> Expenses { get; set; } = new HashSet<Expense>();

        public ICollection<Income> Incomes { get; set; } = new HashSet<Income>();   
    }
}
