namespace Savico.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Savico.Core.Models;
    using Savico.Models.ViewModels.Budget;
    using Savico.Services;
    using Savico.Services.Contracts;
    using System.Security.Claims;

    public class BudgetController : Controller
    {
        private readonly IBudgetService budgetService;

        public BudgetController(IBudgetService budgetService)
        {
            this.budgetService = budgetService;
        }

        [HttpGet]
        public async Task<IActionResult> Index() // should display all budgets
        {
            var budgetViewModels = await budgetService.GetAllBudgetsAsync(); // IEnumerable<BudgetViewModel>

            return View(budgetViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id) // should show details for a specific budget
        {
            var budget = await budgetService.GetBudgetByIdAsync(id);

            if (budget == null)
            {
                return BadRequest();
            }

            return View(budget);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var budgetViewModel = new BudgetViewModel();

            return View(budgetViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BudgetViewModel budgetViewModel)
        {
            if (ModelState.IsValid)
            {
                var budget = new Budget
                {
                    TotalAmount = budgetViewModel.TotalAmount,
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };

                await budgetService.AddBudgetAsync(budget);

                return RedirectToAction(nameof(Index));
            }

            return View(budgetViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var budget = await budgetService.GetBudgetByIdAsync(id);

            if (budget == null)
            {
                return NotFound();
            }

            var budgetViewModel = new BudgetViewModel
            {
                Id = budget.Id,
                TotalAmount = budget.TotalAmount
            };

            return View(budgetViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BudgetViewModel budgetViewModel)
        {
            if (id != budgetViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var budget = new Budget
                {
                    Id = budgetViewModel.Id,
                    TotalAmount = budgetViewModel.TotalAmount,
                    UserId = userId 
                };

                await budgetService.UpdateBudgetAsync(budget);
                return RedirectToAction(nameof(Index));
            }

            return View(budgetViewModel); 
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {

            var budget = await budgetService.GetBudgetByIdAsync(id);

            if (budget == null)
            {
                return BadRequest();
            }

            return View(budget);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await budgetService.DeleteBudgetAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
