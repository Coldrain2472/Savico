namespace Savico.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Savico.Core.Models.ViewModels.Income;
    using Savico.Services.Contracts;
    using System.Security.Claims;

    [Authorize]
    public class IncomeController : Controller
    {
        // TO DO: Fix EDIT, somehow I broke it

        private readonly IIncomeService incomeService;

        public IncomeController(IIncomeService incomeService)
        {
            this.incomeService = incomeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userId = GetUserId();
            var incomes = await incomeService.GetAllIncomesAsync(userId);
            return View(incomes);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await incomeService.PrepareIncomeInputModelAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IncomeInputViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userId = GetUserId();
            await incomeService.AddIncomeAsync(model, userId);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = GetUserId();
            var income = await incomeService.GetIncomeByIdAsync(id, userId);
            if (income == null)
            {
                return BadRequest();
            }
            return View(income);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IncomeInputViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userId = GetUserId();
            await incomeService.UpdateIncomeAsync(id, model, userId);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            string userId = GetUserId();
            await incomeService.DeleteIncomeAsync(id, userId);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var userId = GetUserId();
            var income = await incomeService.GetIncomeByIdAsync(id, userId);

            if (income == null)
            {
                return BadRequest();
            }

            return View(income);
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }
    }
}