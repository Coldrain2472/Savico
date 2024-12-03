namespace Savico.Services.Contracts
{
    using Savico.Core.Models.ViewModels.Admin.UserManagement;

    public interface IUserService
    {
        Task<IEnumerable<AllUsersViewModel>> GetAllUsersAsync(); // retrieves all users

        Task BanUserAsync(string userId); // bans a user

        Task RemoveBanAsync(string userId); // removes a ban

        Task PromoteUserAsync(string userId); // promotes an user to admin

        Task DemoteAdminUserToUser(string userId); // demotes an admin to user

        Task<IEnumerable<AllUsersViewModel>> GetAllActiveUsersAsync(); // retrieves all active users (not banned and not deleted)

        Task<IEnumerable<AllUsersViewModel>> GetAllInactiveUsersAsync(); // retrieves all inactive users (banned and deleted)

       // Task DeleteUserAsync(string userId); // deletes a user, will probably add this feature later
    }
}
