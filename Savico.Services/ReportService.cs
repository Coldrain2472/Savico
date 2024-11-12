namespace Savico.Services
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Savico.Core.Models;
    using Savico.Core.Models.ViewModels.Report;
    using Savico.Infrastructure;
    using Savico.Infrastructure.Data.Models;
    using Savico.Services.Contracts;

    public class ReportService : IReportService
    {
        private readonly SavicoDbContext context;
        private readonly UserManager<User> userManager;

        public ReportService(SavicoDbContext context)
        {
            this.context = context;
        }

        //public async Task<ReportViewModel> GenerateReportAsync(string userId)
        //{
        //    var report = await context.Reports
        //        .Where(r => r.UserId == userId && r.IsDeleted == false)
        //        .FirstOrDefaultAsync();

        //    if (report == null)
        //    {
        //        return new ReportViewModel();
        //    }

        //    var userCurrency = await context.Users
        //       .Where(u => u.Id == userId)
        //       .Select(u => u.Currency)
        //       .FirstOrDefaultAsync();

        //    var reportViewModel = new ReportViewModel
        //    {
        //        Id = report.Id,
        //        StartDate = report.StartDate,
        //        EndDate = report.EndDate,
        //        TotalIncome = report.TotalIncome,
        //        TotalExpense = report.TotalExpense,
        //        Currency = userCurrency
        //    };

        //    return reportViewModel;
        //}

        public async Task<ReportViewModel> GenerateReportAsync(string userId, DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                throw new ArgumentException("Start Date cannot be later than End Date.");
            }

            var incomes = await context.Incomes
                .Where(i => i.UserId == userId && i.Date >= startDate && i.Date <= endDate && !i.IsDeleted)
                .SumAsync(i => i.Amount);

            var expenses = await context.Expenses
                .Where(e => e.UserId == userId && e.Date >= startDate && e.Date <= endDate && !e.IsDeleted)
                .SumAsync(e => e.Amount);

            var report = new Report
            {
                UserId = userId,
                StartDate = startDate,
                EndDate = endDate,
                TotalIncome = incomes,
                TotalExpense = expenses,
                IsDeleted = false
            };

            context.Reports.Add(report);
            await context.SaveChangesAsync();

            var reportViewModel = new ReportViewModel
            {
                Id = report.Id,
                UserId = report.UserId,
                StartDate = report.StartDate,
                EndDate = report.EndDate,
                TotalIncome = report.TotalIncome,
                TotalExpense = report.TotalExpense
            };

            return reportViewModel;
        }

        public async Task<IEnumerable<ReportViewModel>> GetReportsByUserIdAsync(string userId)
        {
            var reports = await context.Reports
                .Where(r => r.UserId == userId && !r.IsDeleted)
                .Select(r => new ReportViewModel
                {
                    Id = r.Id,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    TotalIncome = r.TotalIncome,
                    TotalExpense = r.TotalExpense
                })
                .ToListAsync();

            return reports;
        }

        public async Task<ReportViewModel> GetReportByIdAsync(int id)
        {
            var report = await context.Reports
                .Where(r => r.Id == id && !r.IsDeleted)
                .FirstOrDefaultAsync();

            if (report == null)
            {
                return null;
            }

            return new ReportViewModel
            {
                Id = report.Id,
                StartDate = report.StartDate,
                EndDate = report.EndDate,
                TotalIncome = report.TotalIncome,
                TotalExpense = report.TotalExpense
            };
        }

        // soft delete
        public async Task<bool> DeleteReportAsync(int id)
        {
            var report = await context.Reports.FindAsync(id);

            if (report == null)
            {
                return false;
            }

            report.IsDeleted = true;
            await context.SaveChangesAsync();
            return true;
        }
    }
}