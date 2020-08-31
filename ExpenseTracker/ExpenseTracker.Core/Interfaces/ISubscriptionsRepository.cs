using ExpenseTracker.Core.CoreModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Interfaces
{
    public interface ISubscriptionsRepository
    {
        Task<List<CoreSubscriptions>> GetSubscriptionsAsync(int userId = 0, string search = null);

        Task<CoreSubscriptions> GetSubscriptionById(int id);

        Task<bool> SubscriptionExistAsync(int id);

        Task<CoreSubscriptions> AddSubscriptionAsync(CoreSubscriptions sub);

        Task UpdateSubscriptionAsync(CoreSubscriptions sub);

        Task RemoveSubscriptionAsync(int id);
    }
}
