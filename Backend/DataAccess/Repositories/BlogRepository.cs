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
    public class BlogRepository : IBlogRepository
    {
        private readonly DataContext _context;
        public BlogRepository(DataContext context)
        {
            _context = context;
        }
        public async Task Add(Blog blog)
        {
            await _context.Blog.AddAsync(blog);
        }
        public async Task<List<Blog>> GetAll()
        {
            return await _context.Blog
                .Include(bp => bp.BlogPosts)
                .Include(p => p.Ratings)
                .Take(100)
                .ToListAsync();
        }
        public async Task<Blog> GetById(int blogId)
        {
            var blog = await _context.Blog
                .Include(bp => bp.BlogPosts)
                .Include(p => p.Ratings)
                .SingleOrDefaultAsync(bp => bp.BlogId == blogId);
            return blog;
        }
        public void Remove(Blog blog)
        {
            _context.Blog.Remove(blog);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
        public async Task Update(Blog blog)
        {
            _context.Update(blog);
        }
    }
}
