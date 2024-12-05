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
            try
            {
                var userId = GetUserId();

                if (userId == null)
                {
                    return Unauthorized();
                }

                var reports = await reportService.GetReportsByUserIdAsync(userId);

                return View(reports);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");

                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Generate()
        {
            try
            {
                var model = new ReportInputViewModel();

                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");

                return View(new ReportInputViewModel());
            }
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
            try
            {
                var report = await reportService.GetReportByIdAsync(id);

                if (report == null)
                {
                    return BadRequest();
                }

                return View(report);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");

                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var report = await reportService.GetReportByIdAsync(id);

                if (report == null)
                {
                    return BadRequest();
                }

                return View(report);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");

                return BadRequest();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await reportService.DeleteReportAsync(id);

                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");

                return BadRequest();
            }
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }
    }
}
