namespace Savico.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Savico.Core.Models.ViewModels.Goal;
    using Savico.Services.Contracts;
    using System.Security.Claims;

    public class GoalController : Controller
    {
        private readonly IGoalService goalService;

        public GoalController(IGoalService goalService)
        {
            this.goalService = goalService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();

            var goals = await goalService.GetAllGoalsAsync(userId);

            return View(goals);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var userId = GetUserId();

            var goal = await goalService.GetGoalByIdAsync(id, userId);

            if (goal != null)
            {
                return View(goal);
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userId = GetUserId();

            var model = await goalService.GetGoalInputViewModelAsync(userId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GoalInputViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.TargetAmount <= 0)
                    {
                        ModelState.AddModelError("TargetAmount", "Target amount must be greater than zero.");
                    }

                    if (model.TargetDate <= DateTime.Now)
                    {
                        ModelState.AddModelError("TargetDate", "Target date must be in the future.");
                    }

                    if (ModelState.IsValid) 
                    {
                        var userId = GetUserId();

                        await goalService.AddGoalAsync(model, userId); 

                        return RedirectToAction(nameof(Index)); 
                    }
                }

                return View(model);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message); 
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
                return View(model); 
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = GetUserId();

            var goal = await goalService.GetGoalForEditAsync(id, userId);

            if (goal != null)
            {
                return View(goal);
            }

            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GoalInputViewModel model)
        {
            var userId = GetUserId();

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                else
                {
                    await goalService.UpdateGoalAsync(id, model, userId);

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();
            var goal = await goalService.GetGoalByIdAsync(id, userId);

            if (goal == null)
            {
                return BadRequest();
            }

            return View(goal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = GetUserId();

            try
            {
                await goalService.DeleteGoalAsync(id, userId);

                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;

                return RedirectToAction("Delete", new { id });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Contribute(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = await goalService.GetGoalContributeViewModelAsync(id, userId);

            if (model == null)
            {
                return BadRequest();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contribute(GoalContributeViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {
                try
                {
                    await goalService.ContributeToGoalAsync(model, userId);

                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(model);
        }
    
        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }
    }
}