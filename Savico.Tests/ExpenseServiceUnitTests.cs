namespace Savico.Tests
{
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using Savico.Core.Models;
    using Savico.Core.Models.ViewModels.Expense;
    using Savico.Infrastructure;
    using Savico.Infrastructure.Data.Models;
    using Savico.Services;
    using Savico.Services.Contracts;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [TestFixture]
    public class ExpenseServiceUnitTests
    {
        private SavicoDbContext context;
        private IExpenseService expenseService;
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

            var options = new DbContextOptionsBuilder<SavicoDbContext>()
              .UseInMemoryDatabase(databaseName: "SavicoInMemoryDb" + Guid.NewGuid().ToString())
              .Options;

            context = new SavicoDbContext(options);

            context.Categories.AddRange(categoryOne, categoryTwo, categoryThree);
            context.Users.Add(user);
            context.Incomes.Add(incomeOne);
            context.AddRange(expenseOne,expenseTwo, expenseThree);
            await context.SaveChangesAsync();

            expenseService = new ExpenseService(context);
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
        public async Task PrepareExpenseInputModelAsync_ShouldReturnCorrectModel()
        {
            // Arrange
            var model = new ExpenseInputViewModel
            {
                Amount = 100,
                Date = new DateTime(2024, 12, 1),
                Description = "Test Expense",
                CategoryId = 1
            };

            // Act
            var result = await expenseService.PrepareExpenseInputModelAsync(model, userId);

            // Assert
            Assert.That(result.Amount.Equals(model.Amount));
            Assert.That(result.Date.Equals(model.Date));
            Assert.That(result.Description.Equals(model.Description));
            Assert.That(result.Currency.Equals("EUR"));
            Assert.IsNotEmpty(result.Categories);  
        }

        [Test]
        public async Task AddExpenseAsync_ShouldAddExpenseSuccessfully()
        {
            // Arrange
            var model = new ExpenseInputViewModel
            {
                Amount = 500,
                Date = new DateTime(2024, 12, 2),
                Description = "Test Expense",
                CategoryId = 2
            };

            // Act
            await expenseService.AddExpenseAsync(model, userId);

            // Assert
            var expense = await context.Expenses.FirstOrDefaultAsync(e => e.UserId == userId && e.Description == "Test Expense");

            Assert.IsNotNull(expense);
            Assert.That(expense.Amount.Equals(model.Amount));
            Assert.That(expense.Description.Equals(model.Description));
        }

        [Test]
        public async Task GetExpenseForEditAsync_ShouldReturnCorrectExpense()
        {
            // Arrange
            var expenseId = 1; 

            var model = new ExpenseInputViewModel
            {
                Amount = 300,
                Date = new DateTime(2024, 12, 1),
                Description = "Electricity bill",
                CategoryId = 1
            };

            // Act
            var result = await expenseService.GetExpenseForEditAsync(expenseId, userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Description.Equals(model.Description));
            Assert.That(result.Amount.Equals(model.Amount));
            Assert.That(result.Date.Equals(model.Date));
        }

        [Test]
        public async Task GetAllExpensesAsync_ShouldReturnAllExpenses()
        {
            // Act
            var result = await expenseService.GetAllExpensesAsync(userId);

            // Assert
            Assert.IsNotEmpty(result);
            Assert.That(result.Count().Equals(3));
        }

        [Test]
        public async Task GetExpenseByIdAsync_ShouldReturnCorrectExpense()
        {
            // Arrange
            var expenseId = 1; 
            var expectedUserId = userId; 

            // Act
            var result = await expenseService.GetExpenseByIdAsync(expenseId, expectedUserId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id.Equals(expenseId));
        }

        [Test]
        public async Task GetCategoryNameByIdAsync_ShouldReturnCategoryName()
        {
            // Arrange
            var categoryId = 1; 

            // Act
            var result = await expenseService.GetCategoryNameByIdAsync(categoryId);

            // Assert
            Assert.That(result.Equals("Utilities"));
        }

        [Test]
        public async Task UpdateExpenseAsync_ShouldUpdateExpenseSuccessfully()
        {
            // Arrange
            var expenseId = 1;

            var model = new ExpenseInputViewModel
            {
                Amount = 400,
                Description = "Updated description",
                Date = new DateTime(2024, 12, 3),
                CategoryId = 2
            };

            // Act
            await expenseService.UpdateExpenseAsync(expenseId, model, userId);

            // Assert
            var updatedExpense = await context.Expenses.FirstOrDefaultAsync(e => e.Id == expenseId);

            Assert.That(updatedExpense.Amount.Equals(model.Amount));
            Assert.That(updatedExpense.Description.Equals(model.Description));
        }

        [Test]
        public async Task DeleteExpenseAsync_ShouldDeleteExpenseSuccessfully()
        {
            // Act
            await expenseService.DeleteExpenseAsync(1, userId);

            // Assert
            var deletedExpense = await context.Expenses.FindAsync(1);
            Assert.IsTrue(deletedExpense.IsDeleted);
        }

        [Test]
        public async Task CalculateRemainingBudgetAsync_ShouldReturnCorrectRemainingBudget()
        {
            // Act
            var result = await expenseService.CalculateRemainingBudgetAsync(userId);

            // Assert
            Assert.That(result, Is.EqualTo(1585));
        }

        [Test]
        public async Task GetExpensesForPeriodAsync_ShouldReturnExpensesForPeriod()
        {
            // Arrange
            var startDate = new DateTime(2024, 12, 1);
            var endDate = new DateTime(2024, 12, 2);

            // Act
            var result = await expenseService.GetExpensesForPeriodAsync(userId, startDate, endDate);

            // Assert
            Assert.That(result.Count().Equals(2));
            Assert.IsTrue(result.All(e => e.Date >= startDate && e.Date <= endDate));
        }

        [Test]
        public async Task GetExpenseCategories_ShouldReturnCorrectCategories_ForUser()
        {
            // Arrange
            var userId = this.userId;

            // Act
            var result = await expenseService.GetExpenseCategories(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(3)); //  Utilities, Subscription, Groceries

            var utilitiesCategory = result.FirstOrDefault(c => c.Name == "Utilities");
            Assert.IsNotNull(utilitiesCategory);
            Assert.That(utilitiesCategory.TotalAmount, Is.EqualTo(300));

            var subscriptionCategory = result.FirstOrDefault(c => c.Name == "Subscription");
            Assert.IsNotNull(subscriptionCategory);
            Assert.That(subscriptionCategory.TotalAmount, Is.EqualTo(15));

            var groceriesCategory = result.FirstOrDefault(c => c.Name == "Groceries");
            Assert.IsNotNull(groceriesCategory);
            Assert.That(groceriesCategory.TotalAmount, Is.EqualTo(100));
        }

        [Test]
        public async Task FilterExpenses_ByDate_ReturnsMostRecent()
        {
            // Act
            var result = await expenseService.GetFilteredExpensesAsync(userId, "recent");

            // Assert
            Assert.That(result.Count(), Is.EqualTo(3));
            Assert.That(result.First().Date, Is.EqualTo(new DateTime(2024, 12, 1))); 
            Assert.That(result.Last().Date, Is.EqualTo(new DateTime(2024, 11, 30))); 
        }

        [Test]
        public async Task FilterExpenses_ByAmount_ReturnsSortedByAmount()
        {
            // Act
            var result = await expenseService.GetFilteredExpensesAsync(userId, "amount");

            // Assert
            Assert.That(result.Count(), Is.EqualTo(3));
            Assert.That(result.First().Amount, Is.EqualTo(300));
            Assert.That(result.Last().Amount, Is.EqualTo(15));
        }

        [Test]
        public async Task FilterExpenses_NoFilter_ReturnsAllExpenses()
        {
            // Act
            var result = await expenseService.GetFilteredExpensesAsync(userId, "");

            // Assert
            Assert.That(result.Count(), Is.EqualTo(3));
        }
    }
}