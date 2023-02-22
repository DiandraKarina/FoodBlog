using Application.Abstract;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public async Task Add(User user)
        {
            await _context.User.AddAsync(user);
        }
        public async Task<List<User>> GetAll()
        {
            return await _context.User
                .Include(u => u.Blog)
                .Take(100)
                .ToListAsync();
        }
        public async Task<User> GetById(int userId)
        {
            var user = await _context.User
                .Include(u => u.Blog)
                .SingleOrDefaultAsync(u => u.UserId == userId);
            return user;
        }
        public void Remove(User user)
        {
            _context.User.Remove(user);
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(User user)
        {
            _context.Update(user);
        }
    }
}
