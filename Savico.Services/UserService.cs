namespace Savico.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Savico.Core.Models;
    using Savico.Core.Models.ViewModels.Admin.UserManagement;
    using Savico.Services.Contracts;

    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;

        public UserService(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IEnumerable<AllUsersViewModel>> GetAllUsersAsync() // retrieves all the users
        {
            IEnumerable<User> allUsers = await userManager.Users.ToArrayAsync();

            ICollection<AllUsersViewModel> allUsersViewModel = new List<AllUsersViewModel>();

            foreach (User user in allUsers)
            {
                IEnumerable<string> roles = await userManager.GetRolesAsync(user);

                allUsersViewModel.Add(new AllUsersViewModel()
                {
                    Id = user.Id,
                    Email = user.Email!,
                    Roles = roles
                });
            }

            return allUsersViewModel;
        }

        public async Task BanUserAsync(string userId) // banning user and preventing him to log into his account
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.LockoutEnd = DateTime.UtcNow.AddYears(100); 
                await userManager.UpdateAsync(user);
            }
        }

        public async Task PromoteUserAsync(string userId) // promotes user to admin
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var isInRole = await userManager.IsInRoleAsync(user, "Administrator");
                if (!isInRole)
                {
                    await userManager.AddToRoleAsync(user, "Administrator");
                }
            }
        }
    }
}
