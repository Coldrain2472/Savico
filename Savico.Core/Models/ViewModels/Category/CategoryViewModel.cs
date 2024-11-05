namespace Savico.Core.Models.ViewModels.Category
{
    using System.ComponentModel.DataAnnotations;
    using static Savico.Infrastructure.Data.Constants.DataConstants.ExpenseCategoryConstants;
    using static Savico.Infrastructure.Data.Constants.DataConstants;

    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(CategoryNameMaxLength, MinimumLength = CategoryNameMinLength, ErrorMessage = LengthErrorMessage)]
        public string Name { get; set; } = null!;
    }
}
