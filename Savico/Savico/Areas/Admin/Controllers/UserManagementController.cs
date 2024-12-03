namespace Savico.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AllUsersViewModel> allUsers = await userService.GetAllUsersAsync();

            return View(allUsers);
        }

        [HttpPost]
        public async Task<IActionResult> Ban(string userId)
        {
            await userService.BanUserAsync(userId);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveBan(string userId)
        {
            await userService.RemoveBanAsync(userId);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Promote(string userId)
        {
            await userService.PromoteUserAsync(userId);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Demote(string userId)
        {
            await userService.DemoteAdminUserToUser(userId);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ActiveUsers() 
        {
            var activeUsers = await userService.GetAllActiveUsersAsync();

            return View(activeUsers);
        }

        [HttpGet]
        public async Task<IActionResult> InactiveUsers() 
        {
            var inactiveUsers = await userService.GetAllInactiveUsersAsync();

            return View(inactiveUsers);
        }
    }
}
