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

        [HttpGet]
        public async Task<IActionResult> Index(string filterBy, int pageNumber = 1)
        {
            var userId = GetUserId();

            const int pageSize = 10;

            var (expenses, totalItems) = await expenseService.GetPaginatedExpensesAsync(userId, pageNumber, pageSize, filterBy);

            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            ViewData["FilterOption"] = filterBy;
            ViewData["CurrentPage"] = pageNumber;
            ViewData["TotalPages"] = totalPages;

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
            var userId = GetUserId();

            var model = await expenseService.PrepareExpenseInputModelAsync(new ExpenseInputViewModel(), userId);

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpenseInputViewModel model)
        {
            var userId = GetUserId();

            try
            {
                if (ModelState.IsValid)
                {
                    await expenseService.AddExpenseAsync(model, userId);

                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    model.Categories = await expenseService.GetCategories();
                    return View(model);
                }
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");

            }
            model.Categories = await expenseService.GetCategories();
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

            try
            {
                if (!ModelState.IsValid)
                {
                    model.Categories = await expenseService.GetCategories();
                    return View(model);
                }
                else
                {
                    await expenseService.UpdateExpenseAsync(id, model, userId);

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");

            }
            model.Categories = await expenseService.GetCategories();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();

            var expense = await expenseService.GetExpenseByIdAsync(id, userId);

            if (expense == null)
            {
                return BadRequest();
            }

            return View(expense);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = GetUserId();

            var expense = await expenseService.GetExpenseByIdAsync(id, userId);

            await expenseService.DeleteExpenseAsync(id, userId);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Filter(string filterBy)
        {
            var userId = GetUserId();

            var expenses = await expenseService.GetFilteredExpensesAsync(userId, filterBy);

            return View("Index", expenses);
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }
    }
}
