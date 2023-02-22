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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public async Task Add(Category category)
        {
            await _context.Category.AddAsync(category);
        }
        public async Task<List<Category>> GetAll()
        {
            return await _context.Category
                .Include(u => u.BlogPost)
                .Take(100)
                .ToListAsync();
        }
        public async Task<Category> GetById(int categoryId)
        {
            var category = await _context.Category
                .Include(u => u.BlogPost)
                 .SingleOrDefaultAsync(p => p.CategoryId == categoryId);
            return category;
        }
        public void Remove(Category category)
        {
            _context.Category.Remove(category);
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(Category category)
        {
            _context.Update(category);
        }
    }
}
