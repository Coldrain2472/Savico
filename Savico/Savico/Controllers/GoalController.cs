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

        // TO DO: Fix the functionality

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GoalInputViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = GetUserId();
                await goalService.AddGoalAsync(model, userId);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var userId = GetUserId();
            var goal = await goalService.GetGoalByIdAsync(id, userId);

            if (goal == null)
            {
                return NotFound();
            }

            return View(goal);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();

            var goals = await goalService.GetAllGoalsAsync(userId);

            return View(goals);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();
            await goalService.DeleteGoalAsync(id, userId);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult CalculateMonthlySavings(int goalId)
        {
            var userId = GetUserId();
            var goal = goalService.GetGoalByIdAsync(goalId, userId).Result;

            if (goal == null)
            {
                return NotFound();
            }

            var monthlySavings = goalService.CalculateMonthlySavings(goal.TargetAmount, goal.CurrentAmount, goal.TargetDate);

            ViewData["MonthlySavings"] = monthlySavings;
            return View(goal);
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }
    }
}