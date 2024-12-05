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

            try
            {
                var (expenses, totalItems) = await expenseService.GetPaginatedExpensesAsync(userId, pageNumber, pageSize, filterBy);

                var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

                ViewData["FilterOption"] = filterBy;
                ViewData["CurrentPage"] = pageNumber;
                ViewData["TotalPages"] = totalPages;

                return View(expenses);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");

                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var userId = GetUserId();

            try
            {
                var expense = await expenseService.GetExpenseByIdAsync(id, userId);

                if (expense == null)
                {
                    return NotFound();
                }

                return View(expense);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");

                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userId = GetUserId();

            try
            {
                var model = await expenseService.PrepareExpenseInputModelAsync(new ExpenseInputViewModel(), userId);

                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");

                return View();
            }
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

            try
            {
                var expense = await expenseService.GetExpenseForEditAsync(id, userId);

                if (expense == null)
                {
                    return NotFound();
                }

                return View(expense);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");

                return View();
            }
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

            try
            {
                var expense = await expenseService.GetExpenseByIdAsync(id, userId);

                if (expense == null)
                {
                    return NotFound();
                }

                return View(expense);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");

                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = GetUserId();

            try
            {
                var expense = await expenseService.GetExpenseByIdAsync(id, userId);

                if (expense == null)
                {
                    return NotFound();
                }

                await expenseService.DeleteExpenseAsync(id, userId);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Filter(string filterBy)
        {
            var userId = GetUserId();

            try
            {
                var expenses = await expenseService.GetFilteredExpensesAsync(userId, filterBy);

                return View("Index", expenses);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");

                return View("Index");
            }
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }
    }
}
