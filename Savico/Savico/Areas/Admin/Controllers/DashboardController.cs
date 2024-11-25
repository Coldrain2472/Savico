namespace Savico.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Savico.Core.Models;

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        //private readonly UserManager<User> userManager;

        //public DashboardController(UserManager<User> userManager)
        //{
        //    this.userManager = userManager;
        //}

        //public async Task<IActionResult> Index()
        //{
        //    var totalUsers = await userManager.Users.CountAsync();
        //    var activeUsers = await userManager.Users
        //        .CountAsync(u => !u.IsDeleted);

        //    var model = new DashboardViewModel
        //    {
        //        TotalUsers = totalUsers,
        //        ActiveUsers = activeUsers
        //    };

        //    return View(model);
        //}

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
