using ExpenseTracker.Core.CoreModels;
using ExpenseTracker.Core.Interfaces;
using ExpenseTracker.DataAccess.DataModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.DataAccess.Repositories
{
    public class BillRepository : IBillRepository, ISave
    {
        private readonly ShoelessJoeContext _context;

        public BillRepository(ShoelessJoeContext context)
        {
            _context = context;
        }

        public async Task<CoreBills> AddBillAsync(CoreBills bill)
        {
            var newBill = Mapper.MapBills(bill);
            _context.Bills.Add(newBill);

            await Save();

            return Mapper.MapBills(newBill);
        }

        public async Task<bool> BillExistAsync(int id)
        {
            return await _context.Bills.AnyAsync(b => b.BillId == id);
        }

        public async Task<CoreBills> GetBillById(int id)
        {
            var bill = await _context.Bills
                .Include(u => u.User)
                .FirstOrDefaultAsync(b => b.BillId == id);

            return Mapper.MapBills(bill);
        }

        public async Task<List<CoreBills>> GetBillsAsync(string search = null)
        {
            var bills = await _context.Bills
                .Include(u => u.User)
                .ToListAsync();

            if (search == null)
                return bills.Select(Mapper.MapBills).ToList();

            return (bills.FindAll(b =>
                b.User.UserId.ToString().Contains(search.ToLower()) ||
                b.User.FirstName.ToLower().Contains(search.ToLower()) ||
                b.User.LastName.ToLower().Contains(search.ToLower()) ||
                b.BillId.ToString().Contains(search.ToLower()) ||
                b.BillName.ToLower().Contains(search.ToLower()) ||
                b.BillPrice.ToString().Contains(search.ToLower()) ||
                b.DueDate.ToString().Contains(search.ToLower())

                )).Select(Mapper.MapBills).ToList();
        }

        public async Task RemoveBillAsync(int id)
        {
            var bill = await _context.Bills.FindAsync(id);

            if (bill is null)
                return;

            _context.Bills.Remove(bill);
            await Save();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBillAsync(CoreBills bill)
        {
            var newBill = Mapper.MapBills(bill);

            _context.Entry(newBill).State = EntityState.Modified;

            await Save();
        }
    }
}
