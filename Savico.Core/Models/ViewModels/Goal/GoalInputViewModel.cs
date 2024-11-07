namespace Savico.Core.Models.ViewModels.Goal
{
    using System.ComponentModel.DataAnnotations;
    using static Savico.Infrastructure.Data.Constants.DataConstants;

    public class GoalInputViewModel
    {
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = RangeErrorMessage)]
        public decimal TargetAmount { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = RangeErrorMessage)]
        public decimal CurrentAmount { get; set; }

        [Required]
        public DateTime TargetDate { get; set; }
    }
}
