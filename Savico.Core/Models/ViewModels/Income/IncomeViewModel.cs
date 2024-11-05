namespace Savico.Core.Models.ViewModels.Income
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class IncomeViewModel
    {
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string? Source { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
