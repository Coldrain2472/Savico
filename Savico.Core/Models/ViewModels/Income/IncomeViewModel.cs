namespace Savico.Core.Models.ViewModels.Income
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static Savico.Infrastructure.Data.Constants.DataConstants;

    public class IncomeViewModel
    {
        public int Id { get; set; }

        [Required]
        [Range(0.00, double.MaxValue, ErrorMessage = RangeErrorMessage)]
        public decimal Amount { get; set; }

        [Required]
        public string? Source { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string? Currency { get; set; }
    }
}
