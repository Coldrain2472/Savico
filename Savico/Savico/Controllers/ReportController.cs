namespace Savico.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Savico.Core.Models.ViewModels.Report;
    using Savico.Services.Contracts;
    using System.Security.Claims;

    [Authorize]
    public class ReportController : Controller
    {
        private readonly IReportService reportService;

        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var reports = await reportService.GetReportsByUserIdAsync(userId);

            return View(reports);
        }

        [HttpGet]
        public async Task<IActionResult> Generate()
        {
            var model = new ReportInputViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Generate(ReportInputViewModel model)
        {
            try
            {
                if (model.StartDate == DateTime.MinValue || model.EndDate == DateTime.MinValue)
                {
                    ModelState.AddModelError(string.Empty, "Start Date and End Date must be valid dates.");
                }

                if (model.StartDate > model.EndDate)
                {
                    ModelState.AddModelError(string.Empty, "Start Date cannot be later than End Date.");
                }

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var userId = GetUserId();

                if (userId == null)
                {
                    return Unauthorized();
                }

                var report = await reportService.GenerateReportAsync(userId, model.StartDate, model.EndDate);

                return RedirectToAction(nameof(Details), new { id = report.Id });
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
        public async Task<IActionResult> Details(int id)
        {
            var report = await reportService.GetReportByIdAsync(id);

            if (report == null)
            {
                return BadRequest();
            }

            return View(report);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var report = await reportService.GetReportByIdAsync(id);

            if (report == null)
            {
                return BadRequest();
            }

            return View(report);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await reportService.DeleteReportAsync(id);

            if (result)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest();
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }
    }
}
