namespace Savico.Services.Contracts
{
    using Savico.Core.Models.ViewModels.Report;

    public interface IReportService
    {
        Task<ReportViewModel> GenerateReportViewModelAsync(string userId, DateTime startDate, DateTime endDate);
    }
}
