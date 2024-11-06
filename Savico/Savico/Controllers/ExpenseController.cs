namespace Savico.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using Savico.Core.Models.ViewModels.Expense;
    using Savico.Services.Contracts;

    public class ExpenseController : Controller
    {
        private readonly IExpenseService expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            this.expenseService = expenseService;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();

            var expenses = await expenseService.GetAllExpensesAsync(userId);

            return View(expenses);
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var userId = GetUserId();

            var expense = await expenseService.GetExpenseByIdAsync(id, userId);

            return View(expense);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await expenseService.PrepareExpenseInputModelAsync();

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpenseInputViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = GetUserId(); 
                await expenseService.AddExpenseAsync(model, userId); 

                return RedirectToAction(nameof(Index)); 
            }

            return View(model); 
        }
        
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = GetUserId();

            var expense = await expenseService.GetExpenseByIdAsync(id, userId);

            return View(expense);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ExpenseInputViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = GetUserId();

            await expenseService.EditExpenseAsync(id, model, userId);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost("Delete/{id}")]
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
