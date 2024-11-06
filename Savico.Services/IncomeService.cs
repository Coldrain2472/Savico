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
    using Microsoft.EntityFrameworkCore.Storage;

    public class IncomeService : IIncomeService
    {
        private readonly SavicoDbContext context;

        public IncomeService(SavicoDbContext context)
        {
            this.context = context;
        }

        public async Task AddIncomeAsync(IncomeInputViewModel model, string userId)
        {
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
            if (income != null && income.UserId == userId)
            {
                context.Incomes.Remove(income);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<IncomeViewModel>> GetAllIncomesAsync(string userId)
        {
            return await context.Incomes
                .Where(i => i.UserId == userId)
                .Select(i => new IncomeViewModel
                {
                    Id = i.Id,
                    Amount = i.Amount,
                    Source = i.Source,
                    Date = i.Date
                })
                .ToListAsync();
        }

        public async Task<IncomeViewModel> GetIncomeByIdAsync(int incomeId, string userId)
        {
            var income = await context.Incomes.FindAsync(incomeId);
            if (income != null && income.UserId == userId)
            {
                return new IncomeViewModel
                {
                    Id = income.Id,
                    Amount = income.Amount,
                    Source = income.Source,
                    Date = income.Date
                };
            }
            return null;
        }

        public async Task UpdateIncomeAsync(int incomeId, IncomeInputViewModel model, string userId)
        {
            var income = await context.Incomes
        .FirstOrDefaultAsync(i => i.Id == incomeId && i.UserId == userId);

            //var income = await context.Incomes.FindAsync(incomeId);
            if (income != null && income.UserId == userId)
            {
                income.Amount = model.Amount;
                income.Source = model.Source;
                income.Date = model.Date;

                //context.Incomes.Update(income);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IncomeInputViewModel> PrepareIncomeInputModelAsync()
        {
            return new IncomeInputViewModel();
        }


        public async Task<IncomeInputViewModel> GetIncomeForEditAsync(int id, string userId)
        {
            var income = await context.Incomes
                .FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);

            if (income == null)
            {
                return null;
            }

            return new IncomeInputViewModel
            {
                Id = income.Id,
                Amount = income.Amount,
                Source = income.Source,
                Date = income.Date,
            };
        }
    }
}