namespace Savico.Services
{
    using Microsoft.EntityFrameworkCore;
    using Savico.Core.Models.ViewModels.Report;
    using Savico.Infrastructure;
    using Savico.Services.Contracts;

    public class ReportService : IReportService
    {
        private readonly SavicoDbContext context;

        public ReportService(SavicoDbContext context)
        {
            this.context = context;
        }

        public async Task<ReportViewModel> GenerateReportViewModelAsync(string userId, DateTime startDate, DateTime endDate)
        {
            var incomes = await context.Incomes
                                        .Where(i => i.UserId == userId && i.Date >= startDate && i.Date <= endDate)
                                        .Select(i => new IncomeViewModel
                                        {
                                            Date = i.Date,
                                            Amount = i.Amount,
                                            Source = i.Source
                                        })
                                        .ToListAsync();

            var expenses = await context.Expenses
                                         .Where(e => e.UserId == userId && e.Date >= startDate && e.Date <= endDate)
                                         .Select(e => new ExpenseViewModel
                                         {
                                             Date = e.Date,
                                             Amount = e.Amount,
                                             Description = e.Description
                                         })
                                         .ToListAsync();

            var reportModel = new ReportViewModel
            {
                StartDate = startDate,
                EndDate = endDate,
                TotalIncome = incomes.Sum(i => i.Amount),
                TotalExpense = expenses.Sum(e => e.Amount),
                Incomes = incomes,
                Expenses = expenses
            };

            return reportModel;
        }

    }
}
