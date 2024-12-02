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
    public class BudgetServiceUnitTests
    {
        private SavicoDbContext context;
        private IBudgetService budgetService;
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
                UserId = userId,
                StartDate = new DateTime(2024, 11, 30),
                EndDate = new DateTime(2024, 12, 31),
                TotalAmount = 1585,
                IsDeleted = false
            };

            var options = new DbContextOptionsBuilder<SavicoDbContext>()
              .UseInMemoryDatabase(databaseName: "SavicoInMemoryDb" + Guid.NewGuid().ToString())
              .Options;

            context = new SavicoDbContext(options);

            context.Categories.AddRange(categoryOne, categoryTwo, categoryThree);
            context.Users.Add(user);
            context.Incomes.Add(incomeOne);
            context.AddRange(expenseOne, expenseTwo, expenseThree);
            context.AddRange(budget);
            await context.SaveChangesAsync();

            budgetService = new BudgetService(context);
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
        public async Task CalculateRemainingBudgetAsync_ShouldReturnCorrectRemainingBudget()
        {
            // Act
            var result = await budgetService.CalculateRemainingBudgetAsync(userId);

            // Assert
            Assert.That(result, Is.EqualTo(1585));
        }

        [Test]
        public async Task CalculateRemainingBudgetAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Act
            var result = await budgetService.CalculateRemainingBudgetAsync("NonExistingUserId");

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task CalculateRemainingBudgetAsync_ShouldReturnCorrectRemainingBudget_WhenNoGoals()
        {
            // Act
            var result = await budgetService.CalculateRemainingBudgetAsync(userId);

            // Assert
            Assert.That(result, Is.EqualTo(1585));
        }

        [Test]
        public async Task CalculateRemainingBudgetAsync_ShouldReturnCorrectRemainingBudget_WhenNoIncome() 
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var income = context.Incomes.FirstOrDefault(i => i.UserId == userId);

            if (income != null)
            {
                context.Incomes.Remove(income);
                await context.SaveChangesAsync();
            }

            // Act
            var result = await budgetService.CalculateRemainingBudgetAsync(userId);

            // Assert
            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        public async Task GetTotalIncomeAsync_ShouldReturnTotalIncome()
        {
            // Act
            var result = await budgetService.GetTotalIncomeAsync(userId);

            // Assert
            Assert.That(result, Is.EqualTo(2000));
        }

        [Test]
        public async Task GetTotalIncomeAsync_ShouldReturnZero_WhenNoIncome()
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var income = context.Incomes.FirstOrDefault(i => i.UserId == userId);

            if (income != null)
            {
                context.Incomes.Remove(income);
                await context.SaveChangesAsync();
            }

            // Act
            var result = await budgetService.GetTotalIncomeAsync(userId);

            // Assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public async Task GetTotalExpenseAsync_ShouldReturnTotalExpense()
        {
            // Act
            var result = await budgetService.GetTotalExpenseAsync(userId);

            // Assert
            Assert.That(result.Equals(415));
        }

        [Test]
        public async Task GetTotalExpenseAsync_ShouldReturnZero_WhenNoExpenses()
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var expenses = context.Expenses.Where(e => e.UserId == userId).ToList();
            context.Expenses.RemoveRange(expenses);
            await context.SaveChangesAsync();

            // Act
            var result = await budgetService.GetTotalExpenseAsync(userId);

            // Assert
            Assert.That(result.Equals(0)); 
        }
    }
}
