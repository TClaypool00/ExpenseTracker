using ExpenseTracker.Core.CoreModels;
using ExpenseTracker.Core.Interfaces;
using ExpenseTracker.DataAccess.DataModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.DataAccess.Repositories
{
    public class SubscriptionsRepository : ISubscriptionsRepository, ISave
    {
        private readonly ShoelessJoeContext _context;

        public SubscriptionsRepository(ShoelessJoeContext context)
        {
            _context = context;
        }

        public async Task<CoreSubscriptions> AddSubscriptionAsync(CoreSubscriptions sub)
        {
            var newSub = Mapper.MapSub(sub);

            _context.Subscriptions.Add(newSub);

            await Save();

            return Mapper.MapSub(newSub);
        }

        public async Task<CoreSubscriptions> GetSubscriptionById(int id)
        {
            var sub = await _context.Subscriptions
                .Include(u => u.User)
                .FirstOrDefaultAsync(s => s.SubId == id);

            return Mapper.MapSub(sub);
        }

        public async Task<List<CoreSubscriptions>> GetSubscriptionsAsync(string useId = null, string search = null)
        {
            if (useId == null)
            {
                var subs = await _context.Subscriptions
                    .Include(u => u.User)
                    .ToListAsync();

                if (search is null)
                    return subs.Select(Mapper.MapSub).ToList();

                return (subs.FindAll(s =>
                    s.CompanyName.ToLower().Contains(search.ToLower()) ||
                    s.DueDate.ToString().Contains(search) ||
                    s.AmountDue.ToString().Contains(search) ||
                    s.User.FirstName.ToLower().Contains(search.ToLower()) ||
                    s.User.LastName.ToLower().Contains(search.ToLower())
                    )).Select(Mapper.MapSub).ToList();
            }
            else
            {
                var subs = await _context.Subscriptions
                    .Include(u => u.User)
                    .Where(a => a.User.UserId == int.Parse(useId))
                    .ToListAsync();

                if (search is null)
                    return subs.Select(Mapper.MapSub).ToList();

                return (subs.FindAll(s =>
                    s.CompanyName.ToLower().Contains(search.ToLower()) ||
                    s.DueDate.ToString().Contains(search) ||
                    s.AmountDue.ToString().Contains(search)
                    )).Select(Mapper.MapSub).ToList();
            }
        }

        public async Task RemoveSubscriptionAsync(int id)
        {
            var sub = await _context.Subscriptions.FindAsync(id);

            if (sub is null)
                return;

            _context.Subscriptions.Remove(sub);

            await Save();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> SubscriptionExistAsync(int id)
        {
            return await _context.Subscriptions.AnyAsync(s => s.SubId == id);
        }

        public async Task UpdateSubscriptionAsync(CoreSubscriptions sub)
        {
            var newSub = Mapper.MapSub(sub);

            _context.Entry(newSub).State = EntityState.Modified;

            await Save();
        }
    }
}
