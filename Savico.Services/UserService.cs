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

        // TO DO: maybe add a logic to remove ban or at least ask for confirmation before banning someone

        public async Task<IEnumerable<AllUsersViewModel>> GetAllUsersAsync() // retrieves all the registered users
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
                    Roles = roles,
                    LockoutEnd = user.LockoutEnd?.DateTime
                });
            }

            return allUsersViewModel;
        }

        public async Task<IEnumerable<AllUsersViewModel>> GetAllActiveUsersAsync() // shows all the active users (not deleted and not banned)
        {
            var users = await userManager.Users
                .AsNoTracking()
                .Where(u => !u.IsDeleted && !u.LockoutEnd.HasValue)
                .ToListAsync();

            var userRoles = new Dictionary<string, IList<string>>();
            foreach (var user in users)
            {
                userRoles[user.Id] = await userManager.GetRolesAsync(user);
            }

            var userViewModels = users.Select(user => new AllUsersViewModel
            {
                Id = user.Id,
                Email = user.Email!,
                Roles = userRoles[user.Id],
            });

            return userViewModels;
        }

        public async Task<IEnumerable<AllUsersViewModel>> GetAllInactiveUsersAsync() // shows all the inactive users (banned and soft deleted)
        {
            var users = await userManager.Users
                .AsNoTracking()
                .Where(u => u.IsDeleted || (u.LockoutEnd.HasValue && u.LockoutEnd > DateTime.UtcNow))
                .ToListAsync();

            var userRoles = new Dictionary<string, IList<string>>();
            foreach (var user in users)
            {
                userRoles[user.Id] = await userManager.GetRolesAsync(user);
            }

            var userViewModels = users.Select(user => new AllUsersViewModel
            {
                Id = user.Id,
                Email = user.Email!,
                Roles = userRoles[user.Id],
                Status = user.IsDeleted ? "Deleted" : (user.LockoutEnd.HasValue && user.LockoutEnd > DateTime.UtcNow ? "Banned" : "Active")
            });

            return userViewModels;
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

        public async Task RemoveBanAsync(string userId) // removes a ban from a banned user
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user != null)
            {
                user.LockoutEnd = null;

                await userManager.UpdateAsync(user);
            }
        }

        public async Task<List<User>> GetBannedUsersAsync() // retrieves a list of banned users
        {
            var allUsers = userManager.Users.ToList();
            var bannedUsers = allUsers.Where(u => u.LockoutEnd > DateTime.UtcNow).ToList();
            return bannedUsers;
        }

        public async Task PromoteUserAsync(string userId) // promotes user to admin
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var isInRole = await userManager.IsInRoleAsync(user, "Admin");
                var isInUserRole = await userManager.IsInRoleAsync(user, "User");
                if (isInUserRole && !isInRole)
                {
                    await userManager.RemoveFromRoleAsync(user, "User");
                    await userManager.AddToRoleAsync(user, "Admin");
                }

                if (!isInUserRole)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }

        public async Task DemoteAdminUserToUser(string userId) // demotes a user from admin to user
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var isInRole = await userManager.IsInRoleAsync(user, "Admin");
                if (isInRole)
                {
                    await userManager.RemoveFromRoleAsync(user, "Admin");
                    await userManager.AddToRoleAsync(user, "User");
                }
            }
        }
    }
}