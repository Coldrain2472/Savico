namespace Savico.Services.Contracts
{
    using Savico.Core.Models.ViewModels.Report;

    public interface IReportService
    {
        Task<ReportViewModel> GenerateReportAsync(string userId, DateTime startDate, DateTime endDate);

        Task<IEnumerable<ReportViewModel>> GetReportsByUserIdAsync(string userId);

        Task<bool> DeleteReportAsync(int id);

        Task<ReportDetailsViewModel> GetReportByIdAsync(int id);
    }
}
