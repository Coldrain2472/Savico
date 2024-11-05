namespace Savico.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Savico.Services.Contracts;
    using Savico.Core.Models.ViewModels.Income;
    using Savico.Core.Models;
    using Savico.Infrastructure.Repositories.Contracts;

    public class IncomeService : IIncomeService
    {
        private readonly IRepository<Income, int> repository;

        public IncomeService(IRepository<Income, int> repository)
        {
            this.repository = repository;
        }

        public async Task AddIncomeAsync(IncomeInputViewModel model, string userId)
        {
            var income = new Income
            {
                Amount = model.Amount,
                Source = model.Source,
                UserId = userId
            };

            await repository.AddAsync(income);
        }

        public async Task DeleteIncomeAsync(int incomeId, string userId)
        {
            var income = await repository.GetByIdAsync(incomeId);
            if (income != null && income.UserId == userId)
            {
                await repository.DeleteAsync(income);
            }
            else
            {
                throw new UnauthorizedAccessException("You cannot delete this income.");
            }
        }

        public async Task<IEnumerable<IncomeViewModel>> GetAllIncomesAsync(string userId)
        {
            var incomes = await repository.FindAsync(i => i.UserId == userId);
            return incomes.Select(i => new IncomeViewModel
            {
                Id = i.Id,
                Amount = i.Amount,
                Source = i.Source
            });
        }

        public async Task<IncomeViewModel> GetIncomeByIdAsync(int incomeId, string userId)
        {
            var income = await repository.GetByIdAsync(incomeId);
            if (income != null && income.UserId == userId)
            {
                return new IncomeViewModel
                {
                    Id = income.Id,
                    Amount = income.Amount,
                    Source = income.Source
                };
            }
            throw new UnauthorizedAccessException("You cannot access this income.");
        }

        public async Task UpdateIncomeAsync(int incomeId, IncomeInputViewModel model, string userId)
        {
            var income = await repository.GetByIdAsync(incomeId);
            if (income != null && income.UserId == userId)
            {
                income.Amount = model.Amount;
                income.Source = model.Source;
                await repository.UpdateAsync(income);
            }
            else
            {
                throw new UnauthorizedAccessException("You cannot update this income.");
            }
        }
    }
}