using ExpenseTracker.Core.CoreModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Interfaces
{
    public interface IBudgetRepository
    {
        Task<List<CoreBudget>> GetBudgetsAsync(string search = null, int userId = 0);

        Task<CoreBudget> GetBudgetById(int id);

        Task<bool> BudgetExistAsync(int id);

        Task<CoreBudget> AddBudgetAsync(CoreBudget budget);

        Task UpdateBudgetAsync(CoreBudget budget);

        Task RemoveBudgetAsync(int id);
    }
}
