namespace Savico.Core.Models.ViewModels.Goal
{
    using System.ComponentModel.DataAnnotations;
    using static Savico.Infrastructure.Data.Constants.DataConstants;
    using static Savico.Infrastructure.Data.Constants.DataConstants.GoalConstants;

    public class GoalInputViewModel
    {
        public int Id { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = RangeErrorMessage)]
        public decimal TargetAmount { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = RangeErrorMessage)]
        public decimal CurrentAmount { get; set; }

        [Required]
        public DateTime TargetDate { get; set; }

        public string? Currency {  get; set; }

        public decimal ContributionAmount {  get; set; }

        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = LengthErrorMessage)]
        public string? Description {  get; set; } 

        public bool IsAchieved { get; set; }
    }
}
