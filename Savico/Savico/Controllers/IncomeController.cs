namespace Savico.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Savico.Core.Models.ViewModels.Income;
    using Savico.Services;
    using Savico.Services.Contracts;
    using System.Security.Claims;

    [Authorize]
    public class IncomeController : Controller
    {
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
            string userId = GetUserId();

            var model = await incomeService.PrepareIncomeInputModelAsync(userId);

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
            var income = await incomeService.GetIncomeForEditAsync(id, userId);
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

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();
            var income = await incomeService.GetIncomeByIdAsync(id, userId);
            if (income == null)
            {
                return BadRequest();
            }

            await incomeService.DeleteIncomeAsync(id, userId);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = GetUserId();
            var income = await incomeService.GetIncomeByIdAsync(id, userId);
            if (income == null)
            {
                return BadRequest();
            }

            await incomeService.DeleteIncomeAsync(id, userId);
            return RedirectToAction(nameof(Index));
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }
    }
}