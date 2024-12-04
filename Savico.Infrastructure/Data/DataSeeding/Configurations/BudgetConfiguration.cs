namespace Savico.Infrastructure.Data.DataSeeding.Configurations
{
    using System.Globalization;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Savico.Core.Models;
    using static Savico.Infrastructure.Data.Constants.DataConstants;

    public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
    {
        public void Configure(EntityTypeBuilder<Budget> builder)
        {
            builder.HasData(this.SeedBudget());
        }

        private IEnumerable<Budget> SeedBudget()
        {
            var userId = "ad5a5ccb-2be1-4866-b151-0f09a58416f6";
            var adminId = "7c55600b-d374-4bfb-8352-a65bf7d44cc1";

            IEnumerable<Budget> budget = new List<Budget>()
            {
                // budget for user
                 new Budget() {Id = 1,  StartDate = DateTime.ParseExact("01.12.2024", DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), EndDate = DateTime.ParseExact("01.01.2025", DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), TotalAmount =  1000000.00m, UserId = userId },
                 // budget for admin
                 new Budget(){Id = 2, StartDate =DateTime.ParseExact("01.12.2024", DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), EndDate = DateTime.ParseExact("01.01.2025", DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), TotalAmount = 1000000.00m, UserId = adminId }
            };

            return budget;
        }
    }
}
