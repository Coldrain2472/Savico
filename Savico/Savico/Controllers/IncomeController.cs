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
            string userId = GetUserById();
            var incomes = await incomeService.GetAllIncomesAsync(userId);
            return View(incomes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IncomeInputViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); 
            }
            string userId = GetUserById();
            await incomeService.AddIncomeAsync(model, userId);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            string userId = GetUserById();
            var income = await incomeService.GetIncomeByIdAsync(id, userId);

            if (income == null)
            {
                return BadRequest();
            }

            var model = new IncomeInputViewModel
            {
                Amount = income.Amount,
                Source = income.Source
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IncomeInputViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userId = GetUserById();
            await incomeService.UpdateIncomeAsync(id, model, userId);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            string userId = GetUserById();
            var income = await incomeService.GetIncomeByIdAsync(id, userId);

            if (income == null)
            {
                return BadRequest();
            }

            return View(income);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string userId = GetUserById();

            await incomeService.DeleteIncomeAsync(id, userId);

            return RedirectToAction(nameof(Index)); 
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            string userId = GetUserById();
            var income = await incomeService.GetIncomeByIdAsync(id, userId);

            if (income == null)
            {
                return BadRequest();
            }

            return View(income);
        }

        private string GetUserById()
        {
            string id = string.Empty;

            if (User != null)
            {
                id = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
            }

            return id;
        }
    }
}
