namespace Savico.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Savico.Core.Models;
	using Savico.Services;
	using Savico.Services.Contracts;

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
			var budgets = await budgetService.GetAllBudgetsAsync();

			return View(budgets);
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
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Budget budget)
		{
			if (ModelState.IsValid)
			{
				await budgetService.AddBudgetAsync(budget);

				return RedirectToAction(nameof(Index));
			}

			return View(budget);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var budget = await budgetService.GetBudgetByIdAsync(id);

			if (budget == null)
			{
				return BadRequest();
			}

			return View(budget);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Budget budget)
		{
			if (id != budget.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				await budgetService.UpdateBudgetAsync(budget);

				return RedirectToAction(nameof(Index));
			}

			return View(budget);
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
