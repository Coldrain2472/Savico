using Savico.Core.Models.ViewModels.Admin.UserManagement;

namespace Savico.Services.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<AllUsersViewModel>> GetAllUsersAsync();

        Task BanUserAsync(string userId);

        Task PromoteUserAsync(string userId);
    }
}
