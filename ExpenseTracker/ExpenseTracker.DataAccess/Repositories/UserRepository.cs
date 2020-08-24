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
    public class UserRepository : IUserRepository, ISave
    {
        private readonly ShoelessJoeContext _context;

        public UserRepository(ShoelessJoeContext context)
        {
            _context = context;
        }

        public async Task<CoreUsers> AddUserAsync(CoreUsers user)
        {
            var newUser = Mapper.MapUsers(user);

            _context.Users.Add(newUser);

            await Save();

            return Mapper.MapUsers(newUser);
        }

        public async Task<CoreUsers> GetUserByEmail(string email)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            return Mapper.MapUsers(user);
        }

        public async Task<CoreUsers> GetUserById(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == id);

            return Mapper.MapUsers(user);
        }

        public async Task<List<CoreUsers>> GetUsersAsync(string search = null)
        {
            var users = await _context.Users
                .ToListAsync();

            if (search is null)
                return users.Select(Mapper.MapUsers).ToList();

            return (users.FindAll(u =>
            u.UserId.ToString().Contains(search) ||
            u.FirstName.ToLower().Contains(search.ToLower()) ||
            u.LastName.ToLower().Contains(search.ToLower()) ||
            u.Street.ToLower().Contains(search.ToLower()) ||
            u.City.ToLower().Contains(search.ToLower()) ||
            u.State.ToLower().Contains(search.ToLower()) ||
            u.Zip.ToString().Contains(search) ||
            u.Email.ToLower().Contains(search.ToLower()) ||
            u.PhoneNumber.ToLower().Contains(search.ToLower()) ||
            u.Salary.ToString().Contains(search)

            )).Select(Mapper.MapUsers).ToList();
        }

        public async Task RemoveUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user is null)
                return;

            _context.Users.Remove(user);

            await Save();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(CoreUsers user)
        {
            var newUser = Mapper.MapUsers(user);

            _context.Entry(newUser).State = EntityState.Modified;

            await Save();
        }

        public Task<bool> UserExistAsync(int id)
        {
            return _context.Users.AnyAsync(u => u.UserId == id);
        }
    }
}
