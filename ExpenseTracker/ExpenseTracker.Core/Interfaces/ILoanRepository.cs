using ExpenseTracker.Core.CoreModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Interfaces
{
    public interface ILoanRepository
    {
        Task<List<CoreLoan>> GetLoanAsync(string search = null, string userId = null, string creditUnion = null);

        Task<CoreLoan> GetLoanById(int id);

        Task<bool> LoanExistAsync(int id);

        Task<CoreLoan> AddLoanAsync(CoreLoan loan);

        Task UpdateLoanAsync(CoreLoan loan);

        Task RemoveLoanAsync(int id);
    }
}