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
            string userEmail = "testuser123@gmail.com";

            User userOne = new User()
            {
                Id = "ad5a5ccb-2be1-4866-b151-0f09a58416f6",
                UserName = userEmail,
                NormalizedUserName = userEmail.ToUpper(),
                Email = userEmail,
                NormalizedEmail = userEmail.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = "Test",
                LastName = "User",
                Currency = "EUR",
                BudgetId = 1,
                EmailConfirmed = true
            };

            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

            userOne.PasswordHash = passwordHasher.HashPassword(userOne, "Test@123");

            string adminEmail = "admin@admin.com";

            User adminUser = new User()
            {
                Id = "7c55600b-d374-4bfb-8352-a65bf7d44cc1",
                UserName = adminEmail,
                NormalizedUserName = adminEmail.ToUpper(),
                Email = adminEmail,
                NormalizedEmail = adminEmail.ToUpper(),
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
