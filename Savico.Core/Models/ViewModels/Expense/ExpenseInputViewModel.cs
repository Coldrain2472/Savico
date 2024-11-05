﻿namespace Savico.Core.Models.ViewModels.Expense
{
    using Savico.Core.Models.ViewModels.Category;
    using System.ComponentModel.DataAnnotations;
    using static Savico.Infrastructure.Data.Constants.DataConstants;

    public class ExpenseInputViewModel
    {
        public int Id { get; set; }

        [Required]
        [Range(0.00, double.MaxValue, ErrorMessage = RangeErrorMessage)]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string? Description { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; } = new HashSet<CategoryViewModel>();

        //public string CategoryName { get; set; } // Name of the category (e.g., Rent, Food)

        public string FormattedDate => Date.ToString("dd MMM yyyy");
    }
}
