using ExpenseTracker.Core.CoreModels;
using ExpenseTracker.Core.Interfaces;
using ExpenseTracker.DataAccess.DataModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.DataAccess.Repositories
{
    public class LoanRepository : ILoanRepository, ISave
    {
        private readonly ShoelessJoeContext _context;

        public LoanRepository(ShoelessJoeContext context)
        {
            _context = context;
        }

        public async Task<CoreLoan> AddLoanAsync(CoreLoan loan)
        {
            var newLoan = Mapper.MapLoan(loan);
            _context.Loan.Add(newLoan);

            await Save();

            return Mapper.MapLoan(newLoan);
        }

        public async Task<List<CoreLoan>> GetLoanAsync(string search = null, int userId = 0, int creditUnionId = 0)
        {
            if (userId == 0)
            {
                var loans = await _context.Loan
                    .Include(u => u.User)
                    .Include(c => c.Union)
                    .ToListAsync();

                if (search is null)
                    return loans.Select(Mapper.MapLoan).ToList();

                return (loans.FindAll(l =>
                l.MonthlyAmountDue.ToString().Contains(search) ||
                l.Deposit.ToString().Contains(search) ||
                l.TotalAmountDue.ToString().Contains(search) ||
                l.User.FirstName.ToLower().Contains(search.ToLower()) ||
                l.User.LastName.ToLower().Contains(search.ToLower()) ||
                l.Union.CreditUnionName.ToLower().Contains(search.ToLower())
                )).Select(Mapper.MapLoan).ToList();
            }
            else
            {
                var loans = await _context.Loan
                    .Include(u => u.User)
                    .Include(c => c.Union)
                    .Where(a => a.User.UserId == userId)
                    .ToListAsync();

                if (search is null)
                    return loans.Select(Mapper.MapLoan).ToList();

                return (loans.FindAll(l =>
                l.MonthlyAmountDue.ToString().Contains(search) ||
                l.Deposit.ToString().Contains(search) ||
                l.TotalAmountDue.ToString().Contains(search) ||
                l.Union.CreditUnionName.ToLower().Contains(search.ToLower())
                )).Select(Mapper.MapLoan).ToList();
            }
        }

        public async Task<CoreLoan> GetLoanById(int id)
        {
            var loan =  await _context.Loan
                .Include(u => u.User)
                .FirstOrDefaultAsync(l => l.LoanId == id);

            return Mapper.MapLoan(loan);
        }

        public async Task<bool> LoanExistAsync(int id)
        {
            return await _context.Loan.AnyAsync(l => l.LoanId == id);
        }

        public async Task RemoveLoanAsync(int id)
        {
            var loan = await _context.Loan.FindAsync(id);

            if (loan is null)
                return;


            _context.Loan.Remove(loan);

            await Save();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLoanAsync(CoreLoan loan)
        {
            var newLoan = Mapper.MapLoan(loan);

            _context.Entry(newLoan).State = EntityState.Modified;

            await Save();
        }
    }
}