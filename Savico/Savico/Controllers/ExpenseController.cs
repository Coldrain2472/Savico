namespace Savico.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using System.Security.Claims;
	using Savico.Core.Models.ViewModels.Expense;
	using Savico.Services.Contracts;
	using Microsoft.AspNetCore.Authorization;
	using Savico.Services;

	[Authorize]
	public class ExpenseController : Controller
	{
		private readonly IExpenseService expenseService;

		public ExpenseController(IExpenseService expenseService)
		{
			this.expenseService = expenseService;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var userId = GetUserId();

			var expenses = await expenseService.GetAllExpensesAsync(userId);

			return View(expenses);
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			var userId = GetUserId();

			var expense = await expenseService.GetExpenseByIdAsync(id, userId);

			return View(expense);
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			var model = await expenseService.PrepareExpenseInputModelAsync(new ExpenseInputViewModel());

			return View(model);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ExpenseInputViewModel model)
		{
			var userId = GetUserId();

			if (ModelState.IsValid)
			{
				await expenseService.AddExpenseAsync(model, userId);

				return RedirectToAction(nameof(Index));
			}

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var userId = GetUserId();

			var expense = await expenseService.GetExpenseForEditAsync(id, userId);

			if (expense == null)
			{
				return BadRequest();
			}

			return View(expense);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, ExpenseInputViewModel model)
		{
			var userId = GetUserId();
			model.Categories = await expenseService.GetCategories();

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			await expenseService.UpdateExpenseAsync(id, model, userId);

			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			var userId = GetUserId();

			await expenseService.DeleteExpenseAsync(id, userId);

			return RedirectToAction(nameof(Index));
		}

		private string GetUserId()
		{
			return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
		}
	}
}
