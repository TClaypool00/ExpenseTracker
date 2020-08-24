using ExpenseTracker.Core.CoreModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<List<CoreUsers>> GetUsersAsync(string search = null);

        Task<CoreUsers> GetUserByEmail(string email);

        Task<CoreUsers> GetUserById(int id);

        Task<bool> UserExistAsync(int id);

        Task<CoreUsers> AddUserAsync(CoreUsers user);

        Task UpdateUserAsync(CoreUsers user);

        Task RemoveUserAsync(int id);
    }
}
