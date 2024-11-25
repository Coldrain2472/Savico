namespace Savico.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Savico.Core.Models;
    using Savico.Core.Models.ViewModels.Admin.UserManagement;
    using Savico.Services.Contracts;

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserManagementController : Controller
    {
        private readonly IUserService userService;

        public UserManagementController(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            // Logic to retrieve all users
            IEnumerable<AllUsersViewModel> allUsers = await userService.GetAllUsersAsync();
            return View(allUsers);
        }

        // Active Users
        public IActionResult ActiveUsers()
        {
            // Logic to retrieve active users
            return View();
        }

        // Inactive Users
        public IActionResult InactiveUsers()
        {
            // Logic to retrieve inactive users
            return View();
        }
    }
}
