namespace Savico.Tests
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using NUnit.Framework;
    using Savico.Core.Models;
    using Savico.Infrastructure;
    using Savico.Services;
    using Savico.Services.Contracts;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserServiceUnitTests
    {
        private SavicoDbContext context;
        private UserManager<User> userManager;
        private IUserService userService;
        private IEnumerable<User> users;
        private User userOne;
        private User userTwo;
        private User bannedUser;
        private User deletedUser;

        [SetUp]
        public async Task Setup()
        {
            userOne = new User()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Lana",
                LastName = "Ray",
                Email = "lana@ray.com",
                Currency = "EUR",
                IsDeleted = false
            };

            userTwo = new User()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Test",
                LastName = "User",
                Email = "test@user.com",
                Currency = "USD",
                IsDeleted = false
            };

            bannedUser = new User()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Banned",
                LastName = "Test User",
                Email = "user@banned.com",
                Currency = "EUR",
                IsDeleted = false,
                LockoutEnd = System.DateTime.UtcNow.AddYears(1)
            };

            deletedUser = new User()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Deleted",
                LastName = "Test User",
                Email = "user@deleted.com",
                Currency = "USD",
                IsDeleted = true
            };

            users = new List<User>() { userOne, userTwo, bannedUser, deletedUser };

            var options = new DbContextOptionsBuilder<SavicoDbContext>()
               .UseInMemoryDatabase(databaseName: "SavicoInMemoryDb" + Guid.NewGuid().ToString())
               .Options;

            context = new SavicoDbContext(options);

            await context.AddRangeAsync(users);
            context.SaveChanges();

            var userStore = new UserStore<User>(context);
            var passwordHasher = new PasswordHasher<User>();
            var optionsUserManager = Options.Create<IdentityOptions>(new IdentityOptions());
            userManager = new UserManager<User>(userStore, optionsUserManager, passwordHasher, null, null, null, null, null, null);

            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context),
                null, null, null, null);

            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            userService = new UserService(userManager);
        }

        [TearDown]
        public async Task Teardown()
        {
            if (context != null)
            {
                await context.Database.EnsureDeletedAsync();
                await context.DisposeAsync();
                userManager.Dispose();
            }
        }

        [Test]
        public async Task GetAllUsersAsync_ShouldReturnAllUsersWithRoles()
        {
            // Arrange
            await userManager.AddToRoleAsync(userOne, "User");
            await userManager.AddToRoleAsync(userTwo, "Admin");

            // Act
            var result = (await userService.GetAllUsersAsync()).ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result.Any(u => u.Email == "lana@ray.com" && u.Roles.Contains("User")));
            Assert.That(result.Any(u => u.Email == "test@user.com" && u.Roles.Contains("Admin")));
        }

        [Test]
        public async Task GetAllActiveUsersAsync_ShouldReturnOnlyActiveUsers()
        {
            // Arrange
            await userManager.AddToRoleAsync(userOne, "User");
            await userManager.AddToRoleAsync(userTwo, "Admin");
            await userManager.AddToRoleAsync(bannedUser, "User");
            await userManager.AddToRoleAsync(deletedUser, "User");

            // Act
            var result = (await userService.GetAllActiveUsersAsync()).ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Any(u => u.Email == "lana@ray.com"));
            Assert.That(result.Any(u => u.Email == "test@user.com"));
        }

        [Test]
        public async Task GetAllInactiveUsersAsync_ShouldReturnOnlyInactiveUsers()
        {
            // Arrange
            await userManager.AddToRoleAsync(userOne, "User");
            await userManager.AddToRoleAsync(userTwo, "Admin");
            await userManager.AddToRoleAsync(bannedUser, "User");
            await userManager.AddToRoleAsync(deletedUser, "User");

            // Act
            var result = (await userService.GetAllInactiveUsersAsync()).ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.Any(u => u.Email == "user@banned.com"));
            Assert.That(result.All(u => u.Status != "Active"));
        }

        [Test]
        public async Task BanUserAsync_ShouldBanUserAndPreventLogin()
        {
            // Arrange
            await userManager.AddToRoleAsync(userOne, "User");

            // Act
            await userService.BanUserAsync(userOne.Id);

            var bannedUserFromDb = await userManager.FindByIdAsync(userOne.Id);

            // Assert
            Assert.That(bannedUserFromDb.LockoutEnd, Is.Not.Null); 
            Assert.That(bannedUserFromDb.LockoutEnd?.DateTime, Is.GreaterThan(DateTime.UtcNow)); 
        }

        [Test]
        public async Task RemoveBanAsync_ShouldRemoveBanFromUser()
        {
            // Arrange
            await userManager.AddToRoleAsync(userOne, "User");
            await userService.BanUserAsync(userOne.Id);

            // Act
            await userService.RemoveBanAsync(userOne.Id);

            var userAfterUnban = await userManager.FindByIdAsync(userOne.Id);

            // Assert
            Assert.That(userAfterUnban.LockoutEnd, Is.Null); 
        }

        [Test]
        public async Task PromoteUserAsync_ShouldPromoteUserToAdmin()
        {
            // Arrange
            await userManager.AddToRoleAsync(userOne, "User");

            // Act
            await userService.PromoteUserAsync(userOne.Id);

            // Assert
            var roles = await userManager.GetRolesAsync(userOne);
            Assert.That(roles.Contains("Admin")); 
        }

        [Test]
        public async Task DemoteAdminUserToUser_ShouldDemoteAdminToUser()
        {
            // Arrange
            await userManager.AddToRoleAsync(userOne, "Admin");

            // Act
            await userService.DemoteAdminUserToUser(userOne.Id);

            // Assert
            var roles = await userManager.GetRolesAsync(userOne);
            Assert.That(roles.Contains("User")); 
            Assert.That(!roles.Contains("Admin")); 
        }
    }
}