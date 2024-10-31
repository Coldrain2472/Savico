namespace Savico.Infrastructure.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using Savico.Core.Models;
    using System.ComponentModel.DataAnnotations;
    using static Savico.Infrastructure.Data.Constants.DataConstants.BudgetCategoryConstants;

    public class BudgetCategory
    {
        [Comment("Budget category identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        [Comment("Category name")]
        // Groceries, Utilities, Entertainment etc...
        public string Name { get; set; } = null!;

        [Comment("Description of the category")]
        public string? Description { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<Budget> Budgets { get; set; } = new HashSet<Budget>();
    }
}
