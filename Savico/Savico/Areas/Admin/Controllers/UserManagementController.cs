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
            try
            {
                IEnumerable<AllUsersViewModel> allUsers = await userService.GetAllUsersAsync();

                return View(allUsers);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while fetching the users. Please try again later.";

                return View(Enumerable.Empty<AllUsersViewModel>());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Ban(string userId)
        {
            try
            {
                await userService.BanUserAsync(userId);

                TempData["SuccessMessage"] = "User banned successfully.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while banning the user. Please try again later.";

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveBan(string userId)
        {
            try
            {
                await userService.RemoveBanAsync(userId);

                TempData["SuccessMessage"] = "Ban removed successfully.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while removing the ban. Please try again later.";

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Promote(string userId)
        {
            try
            {
                await userService.PromoteUserAsync(userId);

                TempData["SuccessMessage"] = "User promoted to admin successfully.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while promoting the user. Please try again later.";

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Demote(string userId)
        {
            try
            {
                await userService.DemoteAdminUserToUser(userId);

                TempData["SuccessMessage"] = "User demoted successfully.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while demoting the user. Please try again later.";

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> ActiveUsers() 
        {
            try
            {
                var activeUsers = await userService.GetAllActiveUsersAsync();

                return View(activeUsers);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while fetching active users. Please try again later.";

                return View(Enumerable.Empty<AllUsersViewModel>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> InactiveUsers() 
        {
            try
            {
                var inactiveUsers = await userService.GetAllInactiveUsersAsync();

                return View(inactiveUsers);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while fetching inactive users. Please try again later.";

                return View(Enumerable.Empty<AllUsersViewModel>());
            }
        }
    }
}
