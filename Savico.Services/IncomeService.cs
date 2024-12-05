namespace Savico.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Savico.Services.Contracts;
    using Savico.Core.Models.ViewModels.Income;
    using Savico.Core.Models;
    using Savico.Infrastructure;
    using Microsoft.EntityFrameworkCore;

    public class IncomeService : IIncomeService
    {
        private readonly SavicoDbContext context;

        public IncomeService(SavicoDbContext context)
        {
            this.context = context;
        }

        public async Task AddIncomeAsync(IncomeInputViewModel model, string userId)
        {
            if (model.Amount <= 0)
            {
                throw new ArgumentException("Income amount must be greater than zero.");
            }

            if (!DateTime.TryParseExact(model.Date.ToString("dd.MM.yyyy"),"dd.MM.yyyy",System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
            {
                throw new ArgumentException("Invalid date format. Please use dd.MM.yyyy.");
            }

            if (parsedDate.Year < 2023)
            {
                throw new ArgumentException("Date must be realistic.");
            }

            var user = await context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }

            var income = new Income
            {
                Amount = model.Amount,
                Source = model.Source,
                Date = parsedDate,
                UserId = userId
            };

            await context.Incomes.AddAsync(income);
            await context.SaveChangesAsync();
        }

        public async Task DeleteIncomeAsync(int incomeId, string userId)
        {
            var income = await context.Incomes.FindAsync(incomeId);

            if (income != null && !income.IsDeleted && income.UserId == userId)
            {
                income.IsDeleted = true;

                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<IncomeViewModel>> GetAllIncomesAsync(string userId)
        {
            var user = await context.Users.FindAsync(userId);

            var currency = user!.Currency;

            var incomes = await context.Incomes
                .Where(i => i.UserId == userId && !i.IsDeleted)
                .Select(i => new IncomeViewModel
                {
                    Id = i.Id,
                    Source = i.Source,
                    Amount = i.Amount,
                    Date = i.Date,
                    Currency = currency!
                })
                .OrderBy(i => i.Date)
                .ToListAsync();

            return incomes;
        }

        public async Task<IncomeViewModel> GetIncomeByIdAsync(int incomeId, string userId)
        {
            var income = await context.Incomes.FindAsync(incomeId);

            var userCurrency = await context.Users
                .Where(u => u.Id == userId)
                .Select(u => u.Currency)
                .FirstOrDefaultAsync();

            if (income != null && income.UserId == userId)
            {
                var incomeModel = new IncomeViewModel
                {
                    Id = income.Id,
                    Amount = income.Amount,
                    Source = income.Source,
                    Date = income.Date,
                    Currency = userCurrency
                };

                return incomeModel;
            }

            return null;
        }

        public async Task UpdateIncomeAsync(int incomeId, IncomeInputViewModel model, string userId)
        {
            var income = await context.Incomes
              .FirstOrDefaultAsync(i => i.Id == incomeId && i.UserId == userId);

            if (model.Amount <= 0)
            {
                throw new ArgumentException("Income amount must be greater than zero.");
            }

            if (!DateTime.TryParseExact(model.Date.ToString("dd.MM.yyyy"), "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
            {
                throw new ArgumentException("Invalid date format. Please use dd.MM.yyyy.");
            }

            if (parsedDate.Year < 2023)
            {
                throw new ArgumentException("Date must be realistic.");
            }

            if (income != null && income.UserId == userId)
            {
                income.Amount = model.Amount;
                income.Source = model.Source;
                income.Date = model.Date;

                context.Incomes.Update(income);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IncomeInputViewModel> PrepareIncomeInputModelAsync(string userId)
        {
            var user = await context.Users.FindAsync(userId);

            var model = new IncomeInputViewModel()
            {
                Currency = user?.Currency
            };

            return model;
        }

        public async Task<IncomeInputViewModel> GetIncomeForEditAsync(int incomeId, string userId)
        {
            var income = await context.Incomes
                .FirstOrDefaultAsync(i => i.Id == incomeId && i.UserId == userId);

            if (income == null)
            {
                return null;
            }

            var userCurrency = await context.Users
              .Where(u => u.Id == userId)
              .Select(u => u.Currency)
              .FirstOrDefaultAsync();

            var model = new IncomeInputViewModel
            {
                Id = income.Id,
                Amount = income.Amount,
                Source = income.Source,
                Date = income.Date,
                Currency = userCurrency
            };

            return model;
        }

        public async Task<IEnumerable<Income>> GetIncomesForPeriodAsync(string userId, DateTime startDate, DateTime endDate)
        {
            var incomes = await context.Incomes
                 .Where(i => i.UserId == userId && i.Date >= startDate && i.Date <= endDate)
                .ToListAsync();

            return incomes;
        }
    }
}