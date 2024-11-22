namespace Savico.Infrastructure.Data.DataSeeding.Configurations
{
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Savico.Core.Models;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
			IEnumerable<User> users = this.CreateUsers();
			builder.HasData(users);
		}

        private IEnumerable<User> CreateUsers()
        {
			PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
			User userOne = new User()
			{
				Id = Guid.NewGuid().ToString(),
				UserName = "TestUser",
				NormalizedUserName = "TESTUSER",
				Email = "testuser123@gmail.com",
				NormalizedEmail = "TESTUSER123@GMAIL.COM",
				SecurityStamp = Guid.NewGuid().ToString(),
				FirstName = "Test" ,
				LastName = "User",
				Currency = "EUR",
				BudgetId = 1,
				EmailConfirmed = true
			};

			userOne.PasswordHash = passwordHasher.HashPassword(userOne, "Test@123");

			User adminUser = new User()
			{
				Id = Guid.NewGuid().ToString(),
				UserName = "Admin",
				NormalizedUserName = "ADMIN",
				Email = "admin@admin.com",
				NormalizedEmail = "ADMIN@ADMIN.COM",
				SecurityStamp = Guid.NewGuid().ToString(),
				FirstName = "TEST",
				LastName = "ADMIN",
				Currency = "USD",
				BudgetId = 2,
				EmailConfirmed = true
			};

			adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin@123");

			List<User> users = new List<User>() { userOne, adminUser };

			return users;
		}

	}
}
