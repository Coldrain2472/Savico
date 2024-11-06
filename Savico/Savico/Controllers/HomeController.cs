namespace Savico.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Savico.Core.Models;
    using Savico.Core.Models.ViewModels.Home;
    using Savico.Models;
    using Savico.Services.Contracts;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IBudgetService budgetService;
        private readonly UserManager<User> userManager;

        public HomeController(ILogger<HomeController> logger, IBudgetService budgetService, UserManager<User> userManager)
        {
            this.logger = logger;
            this.budgetService = budgetService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = userManager.GetUserId(User);

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return BadRequest();
            }

            var totalIncome = await budgetService.GetTotalIncomeAsync(userId);
            var totalExpenses = await budgetService.GetTotalExpenseAsync(userId);
            var budget = await budgetService.CalculateRemainingBudgetAsync(userId);

            var viewModel = new HomeViewModel
            {
                FirstName = user.FirstName!,
                LastName = user.LastName!,
                TotalIncome = totalIncome,
                TotalExpense = totalExpenses,
                Budget = (decimal)budget,
                Currency = user.Currency!
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
