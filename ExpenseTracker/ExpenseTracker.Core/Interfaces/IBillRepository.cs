using ExpenseTracker.Core.CoreModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Interfaces
{
    public interface IBillRepository
    {
        Task<List<CoreBills>>GetBillsAsync(string search = null, int userId = 0);

        //Task<List<CoreBills>> GetBillsByUserIdAsync(string userId = null);

        Task<CoreBills> GetBillById(int id);

        Task<bool> BillExistAsync(int id);

        Task<CoreBills> AddBillAsync(CoreBills bill);

        Task UpdateBillAsync(CoreBills bill);

        Task RemoveBillAsync(int id);
    }
}
