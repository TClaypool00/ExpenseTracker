using ExpenseTracker.Core.CoreModels;
using ExpenseTracker.Core.Interfaces;
using ExpenseTracker.DataAccess.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.DataAccess.Repositories
{
    public class BudgetRepository : IBudgetRepository, ISave
    {
        private readonly ShoelessJoeContext _context;

        public BudgetRepository(ShoelessJoeContext context)
        {
            _context = context;
        }

        public async Task<CoreBudget> AddBudgetAsync(CoreBudget budget)
        {
            var newBudget = Mapper.MapBudget(budget);

            _context.Budget.Add(newBudget);

            await Save();

            return Mapper.MapBudget(newBudget);
        }

        public async Task<bool> BudgetExistAsync(int id)
        {
            return await _context.Budget.AnyAsync(b => b.BudgetId == id);
        }

        public async Task<CoreBudget> GetBudgetById(int id)
        {
            var budget = await _context.Budget
                .Include(u => u.User)
                .FirstOrDefaultAsync(b => b.BudgetId == id);

            return Mapper.MapBudget(budget);
        }

        public async Task<List<CoreBudget>> GetBudgetsAsync(string search = null, string userId = null)
        {
            if (userId == null)
            {
                var budgets = await _context.Budget
                    .Include(u => u.User)
                    .ToListAsync();

                if (search == null)
                    return budgets.Select(Mapper.MapBudget).ToList();

                return (budgets.FindAll(b =>
                    b.User.UserId.ToString().Contains(search.ToLower()) ||
                    b.User.FirstName.ToLower().Contains(search.ToLower()) ||
                    b.User.LastName.ToLower().Contains(search.ToLower()) ||
                    b.BudgetId.ToString().Contains(search.ToLower()) ||
                    b.TotalAmtBills.ToString().Contains(search.ToLower())

                    )).Select(Mapper.MapBudget).ToList();
            }
            else
            {
                var userBudget = _context.Budget
                    .Include(b => b.User)
                    .Where(d => d.User.UserId == int.Parse(userId))
                    .ToList();

                if (search == null)
                    return userBudget.Select(Mapper.MapBudget).ToList();

                return (userBudget.FindAll(f =>
                    f.TotalAmtBills.ToString().Contains(search.ToLower())
                )).Select(Mapper.MapBudget).ToList();
            }
        }

        public async Task RemoveBudgetAsync(int id)
        {
            var budget = await _context.Budget.FindAsync(id);

            if (budget is null)
                return;
            _context.Budget.Remove(budget);

            await Save();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBudgetAsync(CoreBudget budget)
        {
            var newbudget = Mapper.MapBudget(budget);

            _context.Entry(newbudget).State = EntityState.Modified;

            await Save();
        }
    }
}
