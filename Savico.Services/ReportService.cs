namespace Savico.Services
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Savico.Core.Models;
    using Savico.Core.Models.ViewModels.Expense;
    using Savico.Core.Models.ViewModels.Income;
    using Savico.Core.Models.ViewModels.Report;
    using Savico.Infrastructure;
    using Savico.Infrastructure.Data.Models;
    using Savico.Services.Contracts;
    using System.Security.Claims;

    public class ReportService : IReportService
    {
        private readonly SavicoDbContext context;
        private readonly UserManager<User> userManager;

        public ReportService(SavicoDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<ReportViewModel> GenerateReportAsync(string userId, DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                throw new ArgumentException("Start Date cannot be later than End Date.");
            }

            var incomes = await context.Incomes
                .Where(i => i.UserId == userId && i.Date >= startDate && i.Date <= endDate && !i.IsDeleted)
                .OrderBy(i => i.Date)
                .SumAsync(i => i.Amount);

            var expenses = await context.Expenses
                .Where(e => e.UserId == userId && e.Date >= startDate && e.Date <= endDate && !e.IsDeleted)
                .OrderBy(e => e.Date)
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

            var userCurrency = await context.Users
              .Where(u => u.Id == userId)
              .Select(u => u.Currency)
              .FirstOrDefaultAsync();

            var reportModel = new ReportViewModel
            {
                Id = report.Id,
                UserId = report.UserId,
                StartDate = report.StartDate,
                EndDate = report.EndDate,
                TotalIncome = report.TotalIncome,
                TotalExpense = report.TotalExpense,
                Currency = userCurrency
            };

            return reportModel;
        }

        public async Task<IEnumerable<ReportViewModel>> GetReportsByUserIdAsync(string userId)
        {
            var userCurrency = await context.Users
             .Where(u => u.Id == userId)
             .Select(u => u.Currency)
             .FirstOrDefaultAsync();

            var reports = await context.Reports
                .Where(r => r.UserId == userId && !r.IsDeleted)
                .Select(r => new ReportViewModel
                {
                    Id = r.Id,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    TotalIncome = r.TotalIncome,
                    TotalExpense = r.TotalExpense,
                    Currency = userCurrency
                })
                .ToListAsync();

            return reports;
        }

        public async Task<ReportDetailsViewModel> GetReportByIdAsync(int id)
        {
            var report = await context.Reports
                .Where(r => r.Id == id && !r.IsDeleted)
                .FirstOrDefaultAsync();

            if (report == null)
            {
                return null;
            }

            var incomes = await context.Incomes
                .Where(i => i.UserId == report.UserId && i.Date >= report.StartDate && i.Date <= report.EndDate && !i.IsDeleted)
                .Select(i => new IncomeDetailsViewModel
                {
                    Amount = i.Amount,
                    Date = i.Date,
                    Source = i.Source!
                })
                .OrderBy(i=>i.Date)
                .ToListAsync();

            var expenses = await context.Expenses
                .Where(e => e.UserId == report.UserId && e.Date >= report.StartDate && e.Date <= report.EndDate && !e.IsDeleted)
                .Select(e => new ExpenseDetailsViewModel
                {
                    Amount = e.Amount,
                    Date = e.Date,
                    Description = e.Description
                })
                .OrderBy(e=>e.Date)
                .ToListAsync();

            var user = await context.Users
               .Where(u => u.Id == report.UserId)
               .Select(u => new { u.Currency })
               .FirstOrDefaultAsync();

            var reportModel = new ReportDetailsViewModel
            {
                Id = report.Id,
                StartDate = report.StartDate,
                EndDate = report.EndDate,
                TotalIncome = report.TotalIncome,
                TotalExpense = report.TotalExpense,
                Incomes = incomes,
                Expenses = expenses,
                Currency = user!.Currency
            };

            return reportModel;
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