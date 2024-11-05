using Savico.Core.Models;
using Savico.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savico.Services.Contracts
{
    using Savico.Core.Models.ViewModels.Income;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IIncomeService
    {
        Task AddIncomeAsync(IncomeInputViewModel model, string userId);

        Task DeleteIncomeAsync(int incomeId, string userId); 

        Task<IEnumerable<IncomeViewModel>> GetAllIncomesAsync(string userId);

        Task<IncomeViewModel> GetIncomeByIdAsync(int incomeId, string userId);

        Task UpdateIncomeAsync(int incomeId, IncomeInputViewModel model, string userId);
    }
}
