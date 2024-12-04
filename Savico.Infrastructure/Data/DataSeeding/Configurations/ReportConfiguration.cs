namespace Savico.Infrastructure.Data.DataSeeding.Configurations
{
    using System.Globalization;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Savico.Infrastructure.Data.Models;
    using static Savico.Infrastructure.Data.Constants.DataConstants;

    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasData(this.SeedReports());
        }

        private IEnumerable<Report> SeedReports()
        {
            var userId = "ad5a5ccb-2be1-4866-b151-0f09a58416f6";
            var adminId = "7c55600b-d374-4bfb-8352-a65bf7d44cc1";

            IEnumerable<Report> reports = new List<Report>()
            {
                // reports for user
                new Report() {Id = 1, UserId = userId, StartDate = DateTime.ParseExact("01.10.2024", DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), EndDate = DateTime.Parse("01.11.2024", CultureInfo.InvariantCulture, DateTimeStyles.None), TotalIncome = 0, TotalExpense = 0},
                new Report() { Id = 2, UserId = userId, StartDate =DateTime.ParseExact("01.01.2024", DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), EndDate = DateTime.Parse("01.01.2025", CultureInfo.InvariantCulture, DateTimeStyles.None), TotalIncome = 3000, TotalExpense = 700},
                // reports for admin
                  new Report() {Id = 3, UserId = adminId, StartDate = DateTime.ParseExact("01.10.2024", DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), EndDate = DateTime.Parse("01.11.2024", CultureInfo.InvariantCulture, DateTimeStyles.None), TotalIncome = 0, TotalExpense = 0},
                new Report() { Id = 4, UserId = adminId, StartDate =DateTime.ParseExact("01.01.2024", DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), EndDate = DateTime.Parse("01.01.2025", CultureInfo.InvariantCulture, DateTimeStyles.None), TotalIncome = 2500, TotalExpense = 600}
            };
           
            return reports;
        }
    }
}
