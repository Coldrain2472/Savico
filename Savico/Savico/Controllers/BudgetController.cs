namespace Savico.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Savico.Core.Models;
    using Savico.Models.ViewModels.Budget;
    using Savico.Services.Contracts;
    using Microsoft.AspNetCore.Identity;

    public class BudgetController : Controller
    {
        private readonly IBudgetService budgetService;
        private readonly UserManager<User> userManager;

        public BudgetController(IBudgetService budgetService, UserManager<User> userManager)
        {
            this.budgetService = budgetService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = userManager.GetUserId(User);

                if (userId == null)
                {
                    return Unauthorized();
                }

                var remainingBudget = await budgetService.CalculateRemainingBudgetAsync(userId);

                var model = new BudgetViewModel
                {
                    TotalAmount = (decimal)remainingBudget
                };

                return View(model);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return View(new BudgetViewModel());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");

                return View(new BudgetViewModel());
            }
        }

    }
}
