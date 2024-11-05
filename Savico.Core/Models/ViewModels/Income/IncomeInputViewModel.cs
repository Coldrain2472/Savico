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
        public decimal Amount { get; set; }

        [Required]
        [StringLength(SourceMaxLength, MinimumLength = SourceMinLength, ErrorMessage = LengthErrorMessage)]
        public string? Source { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
