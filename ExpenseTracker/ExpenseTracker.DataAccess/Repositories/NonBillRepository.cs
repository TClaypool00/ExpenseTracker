using ExpenseTracker.Core.CoreModels;
using ExpenseTracker.Core.Interfaces;
using ExpenseTracker.DataAccess.DataModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.DataAccess.Repositories
{
    public class NonBillRepository : INonBillsRepository, ISave
    {
        private readonly ShoelessJoeContext _conetext;

        public NonBillRepository(ShoelessJoeContext context)
        {
            _conetext = context;
        }

        public async Task<CoreNonBills> AddNonBillAsync(CoreNonBills bill)
        {
            var newBill = Mapper.MapNonBills(bill);

            _conetext.NonBills.Add(newBill);

            await Save();

            return Mapper.MapNonBills(newBill);
        }

        public async Task<CoreNonBills> GetNonBillById(int id)
        {
            var bill = await _conetext.NonBills
                .Include(u => u.User)
                .FirstOrDefaultAsync(b => b.NonBillId == id);

            return Mapper.MapNonBills(bill);
        }

        public async Task<List<CoreNonBills>> GetNonBillsAsync(string userId = null, string search = null)
        {
            if (userId == null)
            {
                var bills = await _conetext.NonBills
                    .Include(u => u.User)
                    .ToListAsync();

                if (search == null)
                    return bills.Select(Mapper.MapNonBills).ToList();

                return (bills.FindAll(b =>
                    b.StoreName.ToLower().Contains(search.ToLower()) ||
                    b.Price.ToString().Contains(search) ||
                    b.User.FirstName.ToLower().Contains(search.ToLower()) ||
                    b.User.LastName.ToLower().Contains(search.ToLower())
                    )).Select(Mapper.MapNonBills).ToList();
            }
            else
            {
                var bills = await _conetext.NonBills
                    .Include(u => u.User)
                    .Where(a => a.User.UserId == int.Parse(userId))
                    .ToListAsync();

                if (search == null)
                    return bills.Select(Mapper.MapNonBills).ToList();

                return (bills.FindAll(b =>
                    b.StoreName.ToLower().Contains(search.ToLower()) ||
                    b.Price.ToString().Contains(search)
                    )).Select(Mapper.MapNonBills).ToList();
            }
        }

        public async Task<bool> NonBillExistAsync(int id)
        {
            return await _conetext.NonBills.AnyAsync(b => b.NonBillId == id);
        }

        public async Task RemoveNonBillAsync(int id)
        {
            var bill = await _conetext.NonBills.FindAsync(id);

            if (bill is null)
                return;
            _conetext.NonBills.Remove(bill);

            await Save();
        }

        public async Task Save()
        {
            await _conetext.SaveChangesAsync();
        }

        public async Task UpdateNonBillAsync(CoreNonBills bill)
        {
            var newBill = Mapper.MapNonBills(bill);

            _conetext.Entry(newBill).State = EntityState.Modified;

            await Save();
        }
    }
}
