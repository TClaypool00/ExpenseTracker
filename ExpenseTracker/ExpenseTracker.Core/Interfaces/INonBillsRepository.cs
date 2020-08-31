using ExpenseTracker.Core.CoreModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Interfaces
{
    public interface INonBillsRepository
    {
        Task<List<CoreNonBills>> GetNonBillsAsync(int userId = 0, string search = null);

        Task<CoreNonBills> GetNonBillById(int id);

        Task<bool> NonBillExistAsync(int id);

        Task<CoreNonBills> AddNonBillAsync(CoreNonBills bill);

        Task UpdateNonBillAsync(CoreNonBills bill);

        Task RemoveNonBillAsync(int id);
    }
}
