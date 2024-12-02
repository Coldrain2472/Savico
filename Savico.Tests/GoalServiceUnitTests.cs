namespace Savico.Tests
{
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using Savico.Core.Models;
    using Savico.Core.Models.ViewModels.Goal;
    using Savico.Infrastructure;
    using Savico.Infrastructure.Data.Models;
    using Savico.Services;
    using Savico.Services.Contracts;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [TestFixture]
    public class GoalServiceUnitTests
    {
        private SavicoDbContext context;
        private IGoalService goalService;
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
            await context.SaveChangesAsync();

            budgetService = new BudgetService(context);
            goalService = new GoalService(context, budgetService);
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
        public async Task AddGoalAsync_ShouldAddGoal()
        {
            var model = new GoalInputViewModel
            {
                TargetAmount = 1000,
                TargetDate = new DateTime(2025, 06, 15),
                Description = "Saving for vacation"
            };

            await goalService.AddGoalAsync(model, userId);

            var goal = await context.Goals.FirstOrDefaultAsync(g => g.UserId == userId && g.Description == "Saving for vacation");

            Assert.IsNotNull(goal);
            Assert.That(goal.TargetAmount, Is.EqualTo(1000));
            Assert.That(goal.Description, Is.EqualTo("Saving for vacation"));
        }

        [Test]
        public async Task ContributeToGoalAsync_ShouldContributeToGoal()
        {
            var model = new GoalContributeViewModel
            {
                GoalId = 1,
                ContributionAmount = 100
            };

            await goalService.ContributeToGoalAsync(model, userId);

            var goal = await context.Goals.FirstOrDefaultAsync(g => g.Id == 1);
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            Assert.That(goal.CurrentAmount, Is.EqualTo(100));
            Assert.That(user.Budget.TotalAmount, Is.EqualTo(1900));
        }

        [Test]
        public async Task ContributeToGoalAsync_ShouldThrowIfInsufficientBudget()
        {
            var model = new GoalContributeViewModel
            {
                GoalId = 1,
                ContributionAmount = 2000 // more than the remaining budget
            };

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await goalService.ContributeToGoalAsync(model, userId));

            Assert.That(ex.Message, Is.EqualTo("Insufficient budget for contribution."));
        }

        [Test]
        public async Task GetGoalInputViewModelAsync_ShouldReturnGoalInputViewModel()
        {
            var result = await goalService.GetGoalInputViewModelAsync(userId);

            Assert.IsNotNull(result);
            Assert.That(result.Currency, Is.EqualTo("EUR"));
        }

        [Test]
        public async Task GetGoalByIdAsync_ShouldReturnGoal()
        {
            var result = await goalService.GetGoalByIdAsync(1, userId);

            Assert.IsNotNull(result);
            Assert.That(result.Description, Is.EqualTo("Saving for my dentist appointment"));
        }

        [Test]
        public async Task GetGoalForEditAsync_ShouldReturnGoalForEditing()
        {
            var result = await goalService.GetGoalForEditAsync(1, userId);

            Assert.IsNotNull(result);
            Assert.That(result.Description, Is.EqualTo("Saving for my dentist appointment"));
        }

        [Test]
        public async Task GetAllGoalsAsync_ShouldReturnAllGoalsForUser()
        {
            var result = await goalService.GetAllGoalsAsync(userId);

            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Description, Is.EqualTo("Saving for my dentist appointment"));
        }

        [Test]
        public async Task UpdateGoalAsync_ShouldUpdateGoalSuccessfully()
        {
            // Arrange
            var model = new GoalInputViewModel
            {
                Description = "Updated goal for dental appointment",
                TargetDate = new DateTime(2025, 06, 01),
                CurrentAmount = 300,
                TargetAmount = 500
            };

            // Act
            await goalService.UpdateGoalAsync(1, model, userId);

            // Assert
            var updatedGoal = await context.Goals.FirstOrDefaultAsync(g => g.Id == 1);

            Assert.IsNotNull(updatedGoal, "Goal should be found after update.");
            Assert.That(updatedGoal.Description, Is.EqualTo("Updated goal for dental appointment"));
            Assert.That(updatedGoal.TargetDate, Is.EqualTo(new DateTime(2025, 06, 01)));
            Assert.That(updatedGoal.CurrentAmount, Is.EqualTo(300));
            Assert.That(updatedGoal.TargetAmount, Is.EqualTo(500));
            Assert.IsFalse(updatedGoal.IsAchieved);
        }

        [Test]
        public async Task UpdateGoalAsync_ShouldMarkGoalAsAchieved_WhenCurrentAmountIsGreaterThanOrEqualToTargetAmount()
        {
            // Arrange
            var model = new GoalInputViewModel
            {
                Description = "Updated goal for dental appointment",
                TargetDate = new DateTime(2025, 06, 01),
                CurrentAmount = 600,
                TargetAmount = 500
            };

            // Act
            await goalService.UpdateGoalAsync(1, model, userId);

            // Assert
            var updatedGoal = await context.Goals.FirstOrDefaultAsync(g => g.Id == 1);
            Assert.IsNotNull(updatedGoal);
            Assert.IsTrue(updatedGoal.IsAchieved);
        }

        [Test]
        public async Task UpdateGoalAsync_ShouldNotUpdate_WhenGoalDoesNotExist()
        {
            // Arrange
            var nonExistingGoalId = 999;

            var model = new GoalInputViewModel
            {
                Description = "Updated goal for dental appointment",
                TargetDate = new DateTime(2025, 06, 01),
                CurrentAmount = 300,
                TargetAmount = 500
            };

            // Act
            await goalService.UpdateGoalAsync(nonExistingGoalId, model, userId);

            // Assert
            var goal = await context.Goals.FirstOrDefaultAsync(g => g.Id == nonExistingGoalId);
            Assert.IsNull(goal);
        }

        [Test]
        public async Task UpdateGoalAsync_ShouldNotUpdate_WhenUserDoesNotExist()
        {
            // Arrange
            var nonExistingUserId = Guid.NewGuid().ToString(); // Non-existing user

            var model = new GoalInputViewModel
            {
                Description = "Updated goal for dental appointment",
                TargetDate = new DateTime(2025, 06, 01),
                CurrentAmount = 300,
                TargetAmount = 500
            };

            // Act
            await goalService.UpdateGoalAsync(1, model, nonExistingUserId);

            // Assert
            var goal = await context.Goals.FirstOrDefaultAsync(g => g.Id == 1);
            Assert.IsNotNull(goal);
            Assert.That(goal.Description, Is.Not.EqualTo("Updated goal for dental appointment"));
        }

        [Test]
        public async Task UpdateGoalAsync_ShouldNotMarkGoalAsAchieved_WhenCurrentAmountIsNotGreaterThanTargetAmount()
        {
            // Arrange
            var model = new GoalInputViewModel
            {
                Description = "Updated goal for dental appointment",
                TargetDate = new DateTime(2025, 06, 01),
                CurrentAmount = 400,
                TargetAmount = 500
            };

            // Act
            await goalService.UpdateGoalAsync(1, model, userId);

            // Assert
            var updatedGoal = await context.Goals.FirstOrDefaultAsync(g => g.Id == 1);
            Assert.IsNotNull(updatedGoal, "Goal should be found after update.");
            Assert.IsFalse(updatedGoal.IsAchieved, "Goal should not be marked as achieved.");
        }

        [Test]
        public async Task DeleteGoalAsync_ShouldMarkGoalAsDeletedAndUpdateBudget() // TO DO
        {

        }

        [Test]
        public async Task DeleteGoalAsync_ShouldMarkAchievedGoalAsDeletedAndUpdateBudget() // TO DO
        {

        }

        [Test]
        public async Task DeleteGoalAsync_ShouldNotDelete_WhenGoalIsAlreadyDeleted() // TO DO
        {

        }

        [Test]
        public async Task DeleteGoalAsync_ShouldNotDelete_WhenGoalDoesNotBelongToUser()
        {
            // Arrange
            var otherUserId = Guid.NewGuid().ToString();
            var goalId = 1;

            // Act
            await goalService.DeleteGoalAsync(goalId, otherUserId);

            // Assert
            var goal = await context.Goals.FirstOrDefaultAsync(g => g.Id == goalId);

            Assert.IsFalse(goal!.IsDeleted);
        }

        [Test]
        public async Task DeleteGoalAsync_ShouldNotDelete_WhenGoalDoesNotExist()
        {
            // Arrange
            var nonExistingGoalId = 999;

            // Act
            await goalService.DeleteGoalAsync(nonExistingGoalId, userId);

            // Assert
            var goal = await context.Goals.FirstOrDefaultAsync(g => g.Id == nonExistingGoalId);
            Assert.IsNull(goal);
        }
    }
}

