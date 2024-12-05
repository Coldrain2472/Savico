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
        private readonly IIncomeService incomeService;

        public IncomeController(IIncomeService incomeService)
        {
            this.incomeService = incomeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                string userId = GetUserId();

                var incomes = await incomeService.GetAllIncomesAsync(userId);

                return View(incomes);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred while retrieving incomes.");

                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                string userId = GetUserId();

                var model = await incomeService.PrepareIncomeInputModelAsync(userId);

                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred while preparing the income form.");

                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(IncomeInputViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await incomeService.AddIncomeAsync(model, GetUserId());

                return RedirectToAction("Index", "Income");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var userId = GetUserId();

                var income = await incomeService.GetIncomeForEditAsync(id, userId);

                if (income == null)
                {
                    return BadRequest();
                }

                return View(income);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred while retrieving the income.");

                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, IncomeInputViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userId = GetUserId();

            try
            {
                await incomeService.UpdateIncomeAsync(id, model, userId);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userId = GetUserId();

                var income = await incomeService.GetIncomeByIdAsync(id, userId);

                if (income == null)
                {
                    return BadRequest();
                }

                return View(income);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred while retrieving the income to delete.");

                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
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
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred while deleting the income.");

                return RedirectToAction(nameof(Index));
            }
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }
    }
}