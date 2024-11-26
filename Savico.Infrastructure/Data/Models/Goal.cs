namespace Savico.Core.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using static Savico.Infrastructure.Data.Constants.DataConstants;
    using static Savico.Infrastructure.Data.Constants.DataConstants.GoalConstants;

    public class Goal
    {
        [Comment("Goal identifier")]
        public int Id { get; set; }

        [Required]
        public string? UserId { get; set; } 

        public User? User { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = RangeErrorMessage)]
        [Comment("Goal's target amount")]
        public decimal TargetAmount { get; set; }

        [Required]
        [Range(0.00, double.MaxValue, ErrorMessage = RangeErrorMessage)]
        [Comment("User's current amount towards goal")]
        public decimal CurrentAmount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Comment("Target date for reaching the goal")]
        public DateTime TargetDate { get; set; }

        //[Comment("Monthly contribution towards the set goal")]
        //public decimal MonthlyContribution { get; set; } = 0;

        [Comment("Contribution towards the set goal")]
        public decimal ContributionAmount {  get; set; }

        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = LengthErrorMessage)]
        [Comment("Goal description")] // "saving for my next trip to Paris"
        public string? Description {  get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Comment("Last date that a contribution was made on")]
        public DateTime? LastContributionDate { get; set; } // nullable so that we can handle the case when no contribution has been made yet

        [Comment("Indicates if the goal is soft-deleted")]
        public bool IsDeleted { get; set; } = false;

        [Comment("Indicates if the goal is achieved or not")]
        public bool IsAchieved { get; set; } = false;
    }
}
