namespace Savico.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Savico.Core.Models;
    using Savico.Core.Models.ViewModels.Home;
    using Savico.Models;
    using Savico.Services.Contracts;

    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IBudgetService budgetService;
        private readonly UserManager<User> userManager;
        private readonly ITipService tipService;
        private readonly IExpenseService expenseService;

        public HomeController(ILogger<HomeController> logger, IBudgetService budgetService, UserManager<User> userManager, ITipService tipService, IExpenseService expenseService)
        {
            this.logger = logger;
            this.budgetService = budgetService;
            this.userManager = userManager;
            this.tipService = tipService;
            this.expenseService = expenseService;
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

            var expenseCategories = await expenseService.GetExpenseCategories(userId); 
            var expenseCategoryNames = expenseCategories.Select(x => x.Name).ToList();
            var expenseCategoryValues = expenseCategories.Select(x => x.TotalAmount).ToList();

            var viewModel = new HomeViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                TotalIncome = totalIncome,
                TotalExpense = totalExpenses,
                Budget = (decimal)budget,
                Currency = user.Currency,
                ExpenseCategoryNames = expenseCategoryNames,  
                ExpenseCategoryValues = expenseCategoryValues  
            };

            var tip = await tipService.GetRandomTipAsync();
            ViewData["Tip"] = tip;
            return View(viewModel);
        }
         
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            var model = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            if (statusCode == 404 || statusCode == 400)
            {
                return View("Error404", model);
            }
            else if (statusCode == 500)
            {
                return View("Error500", model);
            }
            return View("Error", model);
        }

        // use this method to test the custom error500 view -> write in the URL /Home/Test
        //public IActionResult Test()
        //{
        //    return StatusCode(500);
        //}
    }
}
