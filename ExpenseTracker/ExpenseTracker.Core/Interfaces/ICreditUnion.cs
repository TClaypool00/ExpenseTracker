using ExpenseTracker.Core.CoreModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Interfaces
{
    public interface ICreditUnion
    {
        Task<List<CoreCreditUnion>> GetCreditUnionsAsync(string search = null);

        Task<CoreCreditUnion> GetCreditUnionById(int id);

        Task<bool> CreditUnionExistAsync(int id);

        Task<CoreCreditUnion> AddCreditUnionAsync(CoreCreditUnion union);

        Task UpdateCreditUnionAsync(CoreCreditUnion union);

        Task RemoveCreditUnionAsync(int id);
    }
}
