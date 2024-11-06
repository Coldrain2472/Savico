namespace Savico.Core.Models.ViewModels.Income
{
    using System.ComponentModel.DataAnnotations;
    using static Savico.Infrastructure.Data.Constants.DataConstants.IncomeConstants;
    using static Savico.Infrastructure.Data.Constants.DataConstants;
    using System;

    public class IncomeInputViewModel
    {
        public int Id { get; set; }

        [Required]
        [Range(0.00, double.MaxValue, ErrorMessage = RangeErrorMessage)]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(SourceMaxLength, MinimumLength = SourceMinLength, ErrorMessage = LengthErrorMessage)]
        public string? Source { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string? Currency {  get; set; }
    }
}
