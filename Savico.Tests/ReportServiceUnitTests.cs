namespace Savico.Tests
{
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using Savico.Core.Models;
    using Savico.Infrastructure;
    using Savico.Infrastructure.Data.Models;
    using Savico.Services;
    using Savico.Services.Contracts;
    using System;
    using System.Linq;
    using System.Threading.Tasks;


    [TestFixture]
    public class ReportServiceUnitTests
    {
        private SavicoDbContext context;
        private IReportService reportService;
        private string userId;

        [SetUp]
        public async Task Setup()
        {
            userId = Guid.NewGuid().ToString();

            var user = new User()
            {
                Id = userId,
                FirstName = "Lana",
                LastName = "Ray",
                Email = "lana@ray.com",
                Currency = "EUR",
                IsDeleted = false
            };

            var incomeOne = new Income()
            {
                Id = 1,
                Amount = 2000,
                Date = new DateTime(2024, 12, 1),
                Source = "Salary",
                UserId = userId
            };

            var categoryOne = new Category()
            {
                Id = 1,
                Name = "Utilities"
            };

            var categoryTwo = new Category()
            {
                Id = 2,
                Name = "Subscription"
            };

            var categoryThree = new Category()
            {
                Id = 3,
                Name = "Groceries"
            };

            var expenseOne = new Expense()
            {
                Id = 1,
                Amount = 300,
                Date = new DateTime(2024, 12, 1),
                CategoryId = 1,
                Description = "Electricity bill",
                UserId = userId,
                BudgetId = 1,
                IsDeleted = false
            };

            var expenseTwo = new Expense()
            {
                Id = 2,
                Amount = 15,
                Date = new DateTime(2024, 11, 30),
                CategoryId = 2,
                Description = "Monthly Netflix subscription",
                UserId = userId,
                BudgetId = 1,
                IsDeleted = false
            };

            var expenseThree = new Expense()
            {
                Id = 3,
                Amount = 100,
                Date = new DateTime(2024, 12, 1),
                CategoryId = 3,
                Description = "Bought some products from the local supermarket",
                UserId = userId,
                BudgetId = 1,
                IsDeleted = false
            };

            var budget = new Budget()
            {
                Id = 1,
                TotalAmount = 2000, // this is the budget's initial amount
                UserId = userId
            };

            var goalOne = new Goal()
            {
                Id = 1,
                TargetAmount = 500,
                TargetDate = new DateTime(2025, 05, 05),
                Description = "Saving for my dentist appointment",
                UserId = userId,
                CurrentAmount = 0,
                IsDeleted = false,
                IsAchieved = false
            };

            var reportOne = new Report()
            {
                Id = 1,
                StartDate = new DateTime(2024, 01, 01),
                EndDate = new DateTime(2025, 01, 01),
                IsDeleted = false,
                UserId = userId
            };

            var options = new DbContextOptionsBuilder<SavicoDbContext>()
              .UseInMemoryDatabase(databaseName: "SavicoInMemoryDb" + Guid.NewGuid().ToString())
              .Options;

            context = new SavicoDbContext(options);

            context.Categories.AddRange(categoryOne, categoryTwo, categoryThree);
            context.Users.Add(user);
            context.Incomes.Add(incomeOne);
            context.AddRange(expenseOne, expenseTwo, expenseThree);
            context.Goals.Add(goalOne);
            context.Budgets.Add(budget);
            context.Reports.Add(reportOne);
            await context.SaveChangesAsync();

            reportService = new ReportService(context);
        }

        [TearDown]
        public async Task Teardown()
        {
            if (context != null)
            {
                await context.Database.EnsureDeletedAsync();
                await context.DisposeAsync();
            }
        }

        [Test]
        public async Task GenerateReportAsync_ShouldCreateReport_WhenDatesAreValid()
        {
            // Arrange
            var startDate = new DateTime(2024, 12, 1);
            var endDate = new DateTime(2024, 12, 2);

            // Act
            var report = await reportService.GenerateReportAsync(userId, startDate, endDate);

            // Assert
            Assert.IsNotNull(report);
            Assert.That(report.UserId, Is.EqualTo(userId));
            Assert.That(report.Currency, Is.EqualTo("EUR"));
            Assert.That(report.TotalIncome, Is.EqualTo(2000));
            Assert.That(report.TotalExpense, Is.EqualTo(400));
        }

        [Test]
        public void GenerateReportAsync_ShouldThrowArgumentException_WhenStartDateIsAfterEndDate()
        {
            // Arrange
            var startDate = new DateTime(2024, 12, 2);
            var endDate = new DateTime(2024, 12, 1);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(
                async () => await reportService.GenerateReportAsync(userId, startDate, endDate)
            );
        }

        [Test]
        public async Task GenerateReportAsync_ShouldThrowArgumentException_WhenStartDateIsBefore2023()
        {
            // Arrange
            var startDate = new DateTime(2022, 12, 1);
            var endDate = new DateTime(2024, 12, 1);

            // Act & Assert
            var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
                await reportService.GenerateReportAsync(userId, startDate, endDate)
            );

            Assert.That(exception.Message, Is.EqualTo("Start Date must be a valid date"));
        }

        [Test]
        public async Task GenerateReportAsync_ShouldReturnReport_WhenValidDatesAreProvided()
        {
            // Arrange
            var startDate = new DateTime(2024, 12, 1);
            var endDate = new DateTime(2024, 12, 31);

            // Act
            var result = await reportService.GenerateReportAsync(userId, startDate, endDate);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.UserId, Is.EqualTo(userId));
            Assert.That(result.StartDate, Is.EqualTo(startDate));
            Assert.That(result.EndDate, Is.EqualTo(endDate));
            Assert.That(result.TotalIncome, Is.EqualTo(2000));
            Assert.That(result.TotalExpense, Is.EqualTo(400));
            Assert.That(result.Currency, Is.EqualTo("EUR"));
        }

        [Test]
        public async Task GetReportsByUserIdAsync_ShouldReturnReports_WhenReportsExist()
        {
            // Arrange
            var startDate = new DateTime(2024, 12, 1);
            var endDate = new DateTime(2024, 12, 2);

            await reportService.GenerateReportAsync(userId, startDate, endDate);

            // Act
            var reports = await reportService.GetReportsByUserIdAsync(userId);

            // Assert
            Assert.IsNotNull(reports);
            Assert.IsNotEmpty(reports);
            Assert.That(reports.First().Currency, Is.EqualTo("EUR"));
        }

        [Test]
        public async Task GetReportsByUserIdAsync_ShouldReturnEmptyList_WhenNoReportsExist()
        {
            // Act
            var reports = await reportService.GetReportsByUserIdAsync(Guid.NewGuid().ToString());

            // Assert
            Assert.IsNotNull(reports);
            Assert.IsEmpty(reports);
        }

        [Test]
        public async Task GetReportByIdAsync_ShouldReturnReportDetails_WhenReportExists()
        {
            // Arrange
            var startDate = new DateTime(2024, 12, 1);
            var endDate = new DateTime(2024, 12, 2);
            var report = await reportService.GenerateReportAsync(userId, startDate, endDate);

            // Act
            var reportDetails = await reportService.GetReportByIdAsync(report.Id);

            // Assert
            Assert.IsNotNull(reportDetails, "Report details should not be null.");
            Assert.That(reportDetails.Id, Is.EqualTo(report.Id));
            Assert.That(reportDetails.Expenses.Count, Is.EqualTo(2));
            Assert.That(reportDetails.Incomes.Count, Is.EqualTo(1));
            Assert.That(reportDetails.Currency, Is.EqualTo("EUR"));
        }

        [Test]
        public async Task GetReportByIdAsync_ShouldReturnNull_WhenReportDoesNotExist()
        {
            // Act
            var reportDetails = await reportService.GetReportByIdAsync(999);

            // Assert
            Assert.IsNull(reportDetails);
        }

        [Test]
        public async Task DeleteReportAsync_ShouldMarkReportAsDeleted_WhenReportExists()
        {
            // Arrange
            var startDate = new DateTime(2024, 12, 1);
            var endDate = new DateTime(2024, 12, 2);

            var report = await reportService.GenerateReportAsync(userId, startDate, endDate);

            // Act
            var result = await reportService.DeleteReportAsync(report.Id);

            // Assert
            Assert.IsTrue(result);
            var deletedReport = await context.Reports.FindAsync(report.Id);
            Assert.IsTrue(deletedReport.IsDeleted);
        }

        [Test]
        public async Task DeleteReportAsync_ShouldReturnFalse_WhenReportDoesNotExist()
        {
            // Act
            var result = await reportService.DeleteReportAsync(999);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
