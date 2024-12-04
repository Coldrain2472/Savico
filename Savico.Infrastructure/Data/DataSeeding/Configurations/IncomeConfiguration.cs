namespace Savico.Infrastructure.Data.DataSeeding.Configurations
{
    using System.Globalization;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Savico.Core.Models;
    using static Savico.Infrastructure.Data.Constants.DataConstants;

    public class IncomeConfiguration : IEntityTypeConfiguration<Income>
    {
        public void Configure(EntityTypeBuilder<Income> builder)
        {
            builder.HasData(this.SeedIncomes());
        }

        private IEnumerable<Income> SeedIncomes()
        {
            var userId = "ad5a5ccb-2be1-4866-b151-0f09a58416f6";
            var adminId = "7c55600b-d374-4bfb-8352-a65bf7d44cc1";

            IEnumerable<Income> incomes = new List<Income>()
            {
                // incomes for user
                 new Income() {Id = 1, Amount = 2700, Date = DateTime.ParseExact("01.12.2024",  DateTimeDefaultFormat,CultureInfo.InvariantCulture, DateTimeStyles.None), Source = "Salary", UserId = userId },
                 new Income() { Id = 2, Amount = 200, Date =DateTime.ParseExact("02.12.2024",  DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), Source = "Freelance", UserId=userId },
                 new Income() {Id = 3, Amount = 100, Date = DateTime.ParseExact("03.12.2024",  DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), Source = "Investments", UserId = userId },
                 // incomes for admin
                 new Income() {Id = 4, Amount = 2350, Date = DateTime.ParseExact("01.12.2024",  DateTimeDefaultFormat,CultureInfo.InvariantCulture, DateTimeStyles.None), Source = "Salary", UserId = adminId },
                 new Income() { Id = 5, Amount = 150, Date =DateTime.ParseExact("01.12.2024",  DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), Source = "Freelance", UserId=adminId }
            };

            return incomes;
        }
    }
}
