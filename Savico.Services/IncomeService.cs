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
    using Microsoft.AspNetCore.Identity;

    public class IncomeService : IIncomeService
    {
        private readonly SavicoDbContext context;
        private readonly UserManager<User> userManager;

        public IncomeService(SavicoDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task AddIncomeAsync(IncomeInputViewModel model, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            var userCurrency = user?.Currency;

            var income = new Income
            {
                Amount = model.Amount,
                Source = model.Source,
                Date = model.Date,
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
            var user = await userManager.FindByIdAsync(userId);
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
                return new IncomeViewModel
                {
                    Id = income.Id,
                    Amount = income.Amount,
                    Source = income.Source,
                    Date = income.Date,
                    Currency = userCurrency
                };
            }

            return null;
        }

        public async Task UpdateIncomeAsync(int incomeId, IncomeInputViewModel model, string userId)
        {
            var income = await context.Incomes
              .FirstOrDefaultAsync(i => i.Id == incomeId && i.UserId == userId);

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
            var user = await userManager.FindByIdAsync(userId);

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
    }
}