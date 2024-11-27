namespace Savico.Services.Contracts
{
    using Savico.Core.Models.ViewModels.Admin.UserManagement;

    public interface IUserService
    {
        Task<IEnumerable<AllUsersViewModel>> GetAllUsersAsync();

        Task BanUserAsync(string userId);

        Task PromoteUserAsync(string userId);

        Task DemoteAdminUserToUser(string userId);

        Task<IEnumerable<AllUsersViewModel>> GetAllActiveUsersAsync();

        Task<IEnumerable<AllUsersViewModel>> GetAllInactiveUsersAsync();
    }
}
