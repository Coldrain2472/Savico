namespace Savico.Infrastructure.Data.Models
{
    using Savico.Core.Models;
    using System.ComponentModel.DataAnnotations;
    using static Savico.Infrastructure.Data.Constants.DataConstants.ExpenseCategoryConstants;

    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string? Name { get; set; }

        public ICollection<Expense> Expenses { get; set; } = new HashSet<Expense>();
    }
}
