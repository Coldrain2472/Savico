namespace Savico.Core.Models.ViewModels.Admin.UserManagement
{
    public class AllUsersViewModel
    {
        public string Id { get; set; } = null!;

        public string Email { get; set; } = null!;

        public IEnumerable<string> Roles { get; set; } = null!;

        public string Status { get; set; } = null!;

        public DateTime? LockoutEnd { get; set; }
    }
}
