namespace Savico.Infrastructure.Data.DataSeeding.Configurations
{
    using System.Globalization;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Savico.Core.Models;
    using static Savico.Infrastructure.Data.Constants.DataConstants;

    public class GoalConfiguration : IEntityTypeConfiguration<Goal>
    {
        public void Configure(EntityTypeBuilder<Goal> builder)
        {
            builder.HasData(this.SeedGoals());
        }

        private IEnumerable<Goal> SeedGoals()
        {
            var userId = "ad5a5ccb-2be1-4866-b151-0f09a58416f6";
            var adminId = "7c55600b-d374-4bfb-8352-a65bf7d44cc1";

            IEnumerable<Goal> goals = new List<Goal>()
            {
                // goals for user
                 new Goal() {Id = 1, TargetAmount = 1500, CurrentAmount = 0, Description="Saving for my trip to Denmark", TargetDate = DateTime.ParseExact("15.08.2025", DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), UserId = userId },
                 new Goal() {Id = 2, TargetAmount = 500, CurrentAmount = 0, Description="Saving for my PC upgrade", TargetDate = DateTime.ParseExact("01.03.2025", DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), UserId = userId },
                 // goals for admin
                 new Goal() {Id = 3, TargetAmount = 1500, CurrentAmount = 0, Description="Saving for my trip to Denmark", TargetDate = DateTime.ParseExact("15.08.2025", DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), UserId = adminId },
                 new Goal() {Id = 4, TargetAmount = 500, CurrentAmount = 0, Description="Saving for my PC upgrade", TargetDate = DateTime.ParseExact("01.03.2025", DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), UserId = adminId }
            };
            
            return goals;
        }
    }
}
