namespace Savico.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using Savico.Core.Models.ViewModels.Expense;
    using Savico.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
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
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = GetUserId();

            var expense = await expenseService.GetExpenseForEditAsync(id, userId);

            return View(expense);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ExpenseInputViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = GetUserId();

                await expenseService.UpdateExpenseAsync(id, model, userId);

                return RedirectToAction(nameof(Index)); 
            }

            model.Categories = await expenseService.GetCategories();

            return View(model); 

            // ModelState.AddModelError(string.Empty, "An error occurred while updating the expense.");

            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}

            //var userId = GetUserId();

            //await expenseService.EditExpenseAsync(id, model, userId);

            //return RedirectToAction(nameof(Index));

            //if (ModelState.IsValid)
            //{
            //    var userId = GetUserId();
            //    await expenseService.EditExpenseAsync(model.Id, model, userId);
            //    return RedirectToAction(nameof(Index)); 
            //}

            //model.Categories = await expenseService.GetCategories();

            //return View(model);

            //if (ModelState.IsValid)
            //{
            //    var userId = GetUserId();  // Get the user ID
            //    var success = await expenseService.EditExpenseAsync(id, model, userId);

            //    if (success)
            //    {
            //        return RedirectToAction(nameof(Index));  // Redirect to index on success
            //    }

            //    ModelState.AddModelError(string.Empty, "An error occurred while updating the expense.");
            //}

            //// If validation failed, repopulate the categories dropdown and return to view
            //model.Categories = await expenseService.GetCategories();
            //return View(model);
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
