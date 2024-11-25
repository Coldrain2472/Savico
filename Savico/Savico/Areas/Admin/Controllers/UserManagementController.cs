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
        private readonly UserManager<User> userManager;

        public UserManagementController(IUserService userService, UserManager<User> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<AllUsersViewModel> allUsers = await userService.GetAllUsersAsync();

            return View(allUsers);
        }

        [HttpPost]
        public async Task<IActionResult> Ban(string userId)
        {
            await userService.BanUserAsync(userId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Promote(string userId)
        {
            await userService.PromoteUserAsync(userId);

            return RedirectToAction("Index");
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
