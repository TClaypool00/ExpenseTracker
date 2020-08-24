using ExpenseTracker.Core.CoreModels;
using ExpenseTracker.Core.Interfaces;
using ExpenseTracker.DataAccess.DataModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.DataAccess.Repositories
{
    public class CreditUnionRepository : ICreditUnionRepository, ISave
    {
        private readonly ShoelessJoeContext _context;

        public CreditUnionRepository(ShoelessJoeContext context)
        {
            _context = context;
        }

        public async Task<CoreCreditUnion> AddCreditUnionAsync(CoreCreditUnion union)
        {
            var newUnion = Mapper.MapUnion(union);

            _context.CreditUnion.Add(newUnion);
            await Save();

            return Mapper.MapUnion(newUnion);
        }

        public async Task<bool> CreditUnionExistAsync(int id)
        {
            return await _context.CreditUnion.AnyAsync(c => c.UnionId == id);
        }

        public async Task<CoreCreditUnion> GetCreditUnionById(int id)
        {
            var union = await _context.CreditUnion
                .FirstOrDefaultAsync(c => c.UnionId == id);

            return Mapper.MapUnion(union);
        }

        public async Task<List<CoreCreditUnion>> GetCreditUnionsAsync(string search = null)
        {
            var unions = await _context.CreditUnion
                .ToListAsync();

            if (search == null)
                return unions.Select(Mapper.MapUnion).ToList();

            return (unions.FindAll(c =>
            c.UnionId.ToString().Contains(search.ToLower()) ||
            c.CreditUnionName.ToLower().Contains(search.ToLower()) ||
            c.Street.ToLower().Contains(search.ToLower()) ||
            c.City.ToLower().Contains(search.ToLower()) ||
            c.State.ToLower().Contains(search.ToLower()) ||
            c.Zip.ToString().Contains(search.ToLower())
            )).Select(Mapper.MapUnion).ToList();
        }

        public async Task RemoveCreditUnionAsync(int id)
        {
            var union = await _context.CreditUnion.FindAsync(id);

            if (union is null)
                return;

            _context.CreditUnion.Remove(union);
            await Save();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCreditUnionAsync(CoreCreditUnion union)
        {
            var newUnion = Mapper.MapUnion(union);

            _context.Entry(newUnion).State = EntityState.Modified;

            await Save();
        }
    }
}
