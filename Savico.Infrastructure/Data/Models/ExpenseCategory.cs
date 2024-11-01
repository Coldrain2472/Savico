namespace Savico.Infrastructure.Data.Models
{
    using Savico.Core.Models;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ExpenseCategory
    {
        public int ExpenseId { get; set; }

        [ForeignKey(nameof(ExpenseId))]
        public Expense? Expense { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }

        public bool IsDeleted { get; set; }
    }
}
