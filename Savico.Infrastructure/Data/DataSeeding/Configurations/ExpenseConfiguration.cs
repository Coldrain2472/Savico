namespace Savico.Infrastructure.Data.DataSeeding.Configurations
{
    using System.Globalization;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Savico.Core.Models;
    using static Savico.Infrastructure.Data.Constants.DataConstants;

    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasData(this.SeedExpenses());
        }

        private IEnumerable<Expense> SeedExpenses()
        {
            var userId = "ad5a5ccb-2be1-4866-b151-0f09a58416f6";
            var adminId = "7c55600b-d374-4bfb-8352-a65bf7d44cc1";

            IEnumerable<Expense> expenses = new List<Expense>()
            {
                // expenses for user
                 new Expense() {Id = 1, Amount = 100, Date = DateTime.ParseExact("01.12.2024",DateTimeDefaultFormat ,CultureInfo.InvariantCulture, DateTimeStyles.None), CategoryId = 1, Description = "Electricity bill", BudgetId = 1, UserId = userId },
                 new Expense() {Id = 2, Amount = 150, Date = DateTime.ParseExact("03.12.2024",DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), CategoryId = 3, Description = "Supermarket shopping", BudgetId = 1, UserId = userId },
                 new Expense() {Id = 3, Amount = 5, Date = DateTime.ParseExact("05.12.2024",DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), CategoryId = 10, Description = "Amazon Prime monthly subscription", BudgetId = 1, UserId = userId },
                 new Expense() {Id = 4, Amount = 10, Date = DateTime.ParseExact("10.12.2024",DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), CategoryId = 10, Description = "Netflix monthly subscription", BudgetId = 1, UserId = userId },
                 new Expense() {Id = 5, Amount = 13, Date = DateTime.ParseExact("09.12.2024",DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), CategoryId = 10, Description = "World of Warcraft monthly subscription", BudgetId = 1, UserId = userId },
                 new Expense() {Id = 6, Amount = 100, Date = DateTime.ParseExact("06.12.2024",DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), CategoryId = 11, BudgetId = 1, UserId = userId },
                 new Expense() {Id = 7, Amount = 35, Date = DateTime.ParseExact("11.12.2024",DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), CategoryId = 13, Description = "Bought a new pair of jeans", BudgetId = 1, UserId = userId },
                 new Expense() {Id = 8, Amount = 50, Date = DateTime.ParseExact("03.12.2024",DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), CategoryId = 12, Description = "Donated to a local animal shelter", BudgetId = 1, UserId = userId },
                 new Expense() {Id = 9, Amount = 40, Date = DateTime.ParseExact("01.12.2024",DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), CategoryId = 4, Description = "Bought a new video game from Steam", BudgetId = 1, UserId = userId },
                 new Expense() {Id = 10, Amount = 50, Date = DateTime.ParseExact("11.12.2024",DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), CategoryId = 9, Description = "Went to check my teeth", BudgetId = 1, UserId = userId },
                 new Expense() {Id = 11, Amount = 30, Date = DateTime.ParseExact("04.12.2024",DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), CategoryId = 1, Description = "Water bill", BudgetId = 1, UserId = userId },
                 new Expense() {Id = 12, Amount = 100, Date =DateTime.ParseExact("01.12.2024",DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), CategoryId = 13, Description = "Bought a new pair of sneakers", BudgetId = 1, UserId = userId },
                 new Expense() {Id = 13, Amount = 17, Date = DateTime.ParseExact("02.12.2024",DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), CategoryId = 10, Description = "Disney+ monthly subscription", BudgetId = 1, UserId = userId },
                 // expenses for admin
                 new Expense() {Id = 14, Amount = 80, Date = DateTime.ParseExact("01.12.2024",DateTimeDefaultFormat ,CultureInfo.InvariantCulture, DateTimeStyles.None), CategoryId = 1, Description = "Electricity bill", BudgetId = 1, UserId = adminId },
                 new Expense() {Id = 15, Amount = 100, Date = DateTime.ParseExact("03.12.2024",DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), CategoryId = 3, Description = "Supermarket shopping", BudgetId = 1, UserId = adminId },
                 new Expense() {Id = 16, Amount = 5, Date = DateTime.ParseExact("05.12.2024",DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), CategoryId = 10, Description = "Amazon Prime monthly subscription", BudgetId = 1, UserId = adminId },
                 new Expense() {Id = 17, Amount = 10, Date = DateTime.ParseExact("10.12.2024",DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), CategoryId = 10, Description = "Netflix monthly subscription", BudgetId = 1, UserId = adminId },
                 new Expense() {Id =18, Amount = 13, Date = DateTime.ParseExact("09.12.2024",DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), CategoryId = 10, Description = "World of Warcraft monthly subscription", BudgetId = 1, UserId = adminId },
                 new Expense() {Id = 19, Amount = 70, Date = DateTime.ParseExact("06.12.2024",DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), CategoryId = 11, BudgetId = 1, UserId = adminId },
                 new Expense() {Id = 20, Amount = 35, Date = DateTime.ParseExact("11.12.2024",DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), CategoryId = 13, Description = "Bought a new pair of jeans", BudgetId = 1, UserId = adminId },
                 new Expense() {Id = 21, Amount = 50, Date = DateTime.ParseExact("03.12.2024",DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), CategoryId = 12, Description = "Donated to a local animal shelter", BudgetId = 1, UserId = adminId },
                 new Expense() {Id = 22, Amount = 40, Date = DateTime.ParseExact("01.12.2024",DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), CategoryId = 4, Description = "Bought a new video game from Steam", BudgetId = 1, UserId = adminId },
                 new Expense() {Id = 23, Amount = 50, Date = DateTime.ParseExact("11.12.2024",DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), CategoryId = 9, Description = "Went to check my teeth", BudgetId = 1, UserId = adminId },
                 new Expense() {Id = 24, Amount = 30, Date = DateTime.ParseExact("04.12.2024",DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), CategoryId = 1, Description = "Water bill", BudgetId = 1, UserId = adminId },
                 new Expense() {Id = 25, Amount = 90, Date =DateTime.ParseExact("01.12.2024",DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), CategoryId = 13, Description = "Bought a new pair of sneakers", BudgetId = 1, UserId = adminId },
                 new Expense() {Id = 26, Amount = 27, Date = DateTime.ParseExact("02.12.2024",DateTimeDefaultFormat, CultureInfo.InvariantCulture, DateTimeStyles.None), CategoryId = 10, Description = "Disney+ monthly subscription", BudgetId = 1, UserId = adminId },
            };

            return expenses;
        }
    }
}