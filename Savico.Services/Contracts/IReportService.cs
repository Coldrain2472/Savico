namespace Savico.Services.Contracts
{
    using Savico.Core.Models.ViewModels.Report;

    public interface IReportService
    {
        Task<ReportViewModel> GenerateReportAsync(string userId, DateTime startDate, DateTime endDate); // generates a report

        Task<IEnumerable<ReportViewModel>> GetReportsByUserIdAsync(string userId); // retrieves a report by user id

        Task<bool> DeleteReportAsync(int id); // deletes a report

        Task<ReportDetailsViewModel> GetReportByIdAsync(int id); // retrieves a report by Id
    }
}
