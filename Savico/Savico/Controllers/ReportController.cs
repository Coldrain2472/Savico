namespace Savico.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Savico.Core.Models.ViewModels.Report;
    using Savico.Services;
    using Savico.Services.Contracts;
    using System.Security.Claims;

    public class ReportController : Controller
    {
        private readonly IReportService reportService;

        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        [HttpGet]
        public IActionResult Generate()
        {
            return View(new ReportDateRangeViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Generate(ReportDateRangeViewModel dateRange)
        {
            if (!ModelState.IsValid)
            {
                return View(dateRange);
            }

            var userId = GetUserId();
            var reportViewModel = await reportService.GenerateReportViewModelAsync(userId, dateRange.StartDate, dateRange.EndDate);

            if (reportViewModel == null)
            {
                return BadRequest();
            }

            return View("ReportDetails", reportViewModel);
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }
    }
}
