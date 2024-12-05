namespace Savico.Tests
{
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using Savico.Core.Models;
    using Savico.Core.Models.ViewModels.Income;
    using Savico.Infrastructure;
    using Savico.Services;
    using Savico.Services.Contracts;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [TestFixture]
    public class IncomeServiceUnitTests
    {
        private SavicoDbContext context;
        private IIncomeService incomeService;
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
                UserId = userId,
            };

            var incomeTwo = new Income()
            {
                Id = 2,
                Amount = 200,
                Date = new DateTime(2024, 12, 2),
                Source = "Freelance",
                UserId = userId
            };

            var options = new DbContextOptionsBuilder<SavicoDbContext>()
               .UseInMemoryDatabase(databaseName: "SavicoInMemoryDb" + Guid.NewGuid().ToString())
               .Options;

            context = new SavicoDbContext(options);

            context.Users.Add(user);
            context.AddRange(incomeOne, incomeTwo);
            await context.SaveChangesAsync(); 

            incomeService = new IncomeService(context);
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
        public async Task AddIncomeAsync_ShouldThrowException_WhenAmountIsInvalid()
        {
            // Arrange
            var model = new IncomeInputViewModel
            {
                Amount = 0,
                Source = "Salary",
                Date = DateTime.Now
            };

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
                await incomeService.AddIncomeAsync(model, userId));
        }

        [Test]
        public async Task AddIncomeAsync_ShouldAddIncome_WhenValidData()
        {
            var model = new IncomeInputViewModel
            {
                Amount = 1500,
                Date = new DateTime(2024, 12, 5),
                Source = "Bonus"
            };

            await incomeService.AddIncomeAsync(model, userId);

            var addedIncome = await context.Incomes.FirstOrDefaultAsync(i => i.UserId == userId && i.Amount == 1500);

            // Assert
            Assert.IsNotNull(addedIncome);
            Assert.That(addedIncome.Source, Is.EqualTo("Bonus"));
            Assert.That(addedIncome.Date, Is.EqualTo(new DateTime(2024, 12, 5)));
        }

        [Test]
        public void AddIncomeAsync_ShouldThrowArgumentException_WhenAmountIsZeroOrNegative()
        {
            var model = new IncomeInputViewModel
            {
                Amount = 0,
                Date = new DateTime(2024, 12, 5),
                Source = "Bonus"
            };

            // Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await incomeService.AddIncomeAsync(model, userId));

            Assert.That(ex.Message, Is.EqualTo("Income amount must be greater than zero."));
        }

        [Test]
        public void AddIncomeAsync_ShouldThrowArgumentException_WhenDateIsBefore2023()
        {
            var model = new IncomeInputViewModel
            {
                Amount = 1500,
                Date = new DateTime(2022, 12, 5),
                Source = "Bonus"
            };

            // Assert 
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await incomeService.AddIncomeAsync(model, userId));

            Assert.That(ex.Message, Is.EqualTo("Date must be realistic."));
        }

        [Test]
        public void AddIncomeAsync_ShouldThrowArgumentException_WhenUserNotFound()
        {
            var model = new IncomeInputViewModel
            {
                Amount = 1500,
                Date = new DateTime(2024, 12, 5),
                Source = "Bonus"
            };

            var invalidUserId = Guid.NewGuid().ToString();

            // Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await incomeService.AddIncomeAsync(model, invalidUserId));

            Assert.That(ex.Message, Is.EqualTo("User not found."));
        }

        [Test]
        public async Task AddIncomeAsync_ShouldAddIncomeSuccessfully() 
        {
            // Arrange
            var model = new IncomeInputViewModel
            {
               // Id = 3,
                Amount = 200,
                Source = "Freelance",
                Date = new DateTime(2024, 12, 2)
            };

            // Act
            await incomeService.AddIncomeAsync(model, userId);

            // Assert
            var income = await context.Incomes.FirstOrDefaultAsync(i => i.UserId == userId && i.Source == "Freelance");
            Assert.IsNotNull(income);
            Assert.That(income.Amount, Is.EqualTo(model.Amount));
            Assert.That(income.Source, Is.EqualTo(model.Source));
            Assert.That(income.Date, Is.EqualTo(model.Date));
            Assert.That(income.Id, Is.Not.EqualTo(0));
        }

        [Test]
        public async Task GetAllIncomesAsync_ShouldSortByDateCorrectly()
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            Assert.IsNotNull(user);

            // Act
            var incomes = await incomeService.GetAllIncomesAsync(userId);

            // Assert
            Assert.IsNotNull(incomes);
            Assert.That(incomes.Count(), Is.EqualTo(2));
            var firstIncomeDate = incomes.First().Date;
            var secondIncomeDate = incomes.Last().Date;
            Assert.Less(firstIncomeDate, secondIncomeDate);
        }

        [Test]
        public async Task GetIncomeByIdAsync_ReturnsTheCorrectResult()
        {
            // Arrange
            int incomeOneId = 1;
            int nonExistingIncomeId = -1;

            // Act
            var resultOne = await incomeService.GetIncomeByIdAsync(incomeOneId, userId);
            var resultTwo = await incomeService.GetIncomeByIdAsync(nonExistingIncomeId, userId);

            // Assert
            Assert.IsNotNull(resultOne);
            Assert.That(resultOne.Id, Is.EqualTo(incomeOneId));
            Assert.IsNull(resultTwo);
        }

        [Test]
        public async Task GetIncomeByIdAsync_ShouldReturnCorrectIncome()
        {
            // Act
            var result = await incomeService.GetIncomeByIdAsync(1, userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id.Equals(1));
            Assert.That(result.Source.Equals("Salary"));
        }

        [Test]
        public async Task UpdateIncomeAsync_ShouldUpdateIncomeSuccessfully()
        {
            // Arrange
            var model = new IncomeInputViewModel
            {
                Amount = 3000,
                Source = "Freelance",
                Date = new DateTime(2024, 12, 3)
            };

            // Act
            await incomeService.UpdateIncomeAsync(1, model, userId);

            // Assert
            var updatedIncome = await context.Incomes.FindAsync(1);
            Assert.That(updatedIncome.Amount.Equals(model.Amount));
            Assert.That(updatedIncome.Source.Equals(model.Source));
            Assert.That(updatedIncome.Date.Equals(model.Date));
        }

        [Test]
        public async Task UpdateIncomeAsync_ShouldUpdateIncome_WhenValidData()
        {
            var model = new IncomeInputViewModel
            {
                Amount = 2500,
                Date = new DateTime(2024, 12, 3),
                Source = "Bonus"
            };

            await incomeService.UpdateIncomeAsync(1, model, userId);

            var updatedIncome = await context.Incomes.FirstOrDefaultAsync(i => i.Id == 1 && i.UserId == userId);

            Assert.IsNotNull(updatedIncome);
            Assert.That(updatedIncome.Amount, Is.EqualTo(2500));
            Assert.That(updatedIncome.Source, Is.EqualTo("Bonus"));
            Assert.That(updatedIncome.Date, Is.EqualTo(new DateTime(2024, 12, 3)));
        }

        [Test]
        public void UpdateIncomeAsync_ShouldThrowArgumentException_WhenAmountIsZeroOrNegative()
        {
            var model = new IncomeInputViewModel
            {
                Amount = 0,
                Date = new DateTime(2024, 12, 3),
                Source = "Bonus"
            };

            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await incomeService.UpdateIncomeAsync(1, model, userId));

            Assert.That(ex.Message, Is.EqualTo("Income amount must be greater than zero."));
        }

        [Test]
        public void UpdateIncomeAsync_ShouldThrowArgumentException_WhenDateIsBefore2023()
        {
            var model = new IncomeInputViewModel
            {
                Amount = 2500,
                Date = new DateTime(2022, 12, 3),
                Source = "Bonus"
            };

            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await incomeService.UpdateIncomeAsync(1, model, userId));

            Assert.That(ex.Message, Is.EqualTo("Date must be realistic."));
        }

        [Test]
        public async Task UpdateIncomeAsync_ShouldNotUpdate_WhenIncomeNotFound()
        {
            var model = new IncomeInputViewModel
            {
                Amount = 2500,
                Date = new DateTime(2024, 12, 3),
                Source = "Bonus"
            };

            await incomeService.UpdateIncomeAsync(999, model, userId);

            var income = await context.Incomes.FirstOrDefaultAsync(i => i.Id == 999 && i.UserId == userId);

            Assert.IsNull(income);
        }


        [Test]
        public async Task PrepareIncomeInputModelAsync_ShouldReturnCorrectModel()
        {
            // Act
            var result = await incomeService.PrepareIncomeInputModelAsync(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Currency.Equals("EUR"));
        }

        [Test]
        public async Task GetIncomeForEditAsync_ShouldReturnIncomeForEditing()
        {
            // Act
            var result = await incomeService.GetIncomeForEditAsync(1, userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id.Equals(1));
            Assert.That(result.Source.Equals("Salary"));
        }

        [Test]
        public async Task GetIncomesForPeriodAsync_ShouldReturnIncomesForTheGivenPeriod()
        {
            // Act
            var startDate = new DateTime(2024, 12, 1);
            var endDate = new DateTime(2024, 12, 2);
            var result = await incomeService.GetIncomesForPeriodAsync(userId, startDate, endDate);

            // Assert
            Assert.That(result.Count().Equals(2));
        }

        [Test]
        public async Task DeleteIncomeAsync_ShouldMarkIncomeAsDeleted()
        {
            // Act
            await incomeService.DeleteIncomeAsync(1, userId);

            // Assert
            var deletedIncome = await context.Incomes.FindAsync(1);
            Assert.IsTrue(deletedIncome.IsDeleted);
        }
    }
}
