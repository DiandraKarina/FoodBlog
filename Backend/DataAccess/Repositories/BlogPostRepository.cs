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
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly DataContext _context;
        public BlogPostRepository(DataContext context)
        {
            _context = context;
        }
        public async Task Add(BlogPost blogPost)
        {
            await _context.BlogPost.AddAsync(blogPost);
        }
        public async Task<List<BlogPost>> GetAll()
        {
            return await _context.BlogPost
                .Include(p => p.Comments)
                .Include(p => p.PostRatings)
                .Take(100)
                .ToListAsync();
        }
        public async Task<List<Comment>> GetCommentsByPostId(int blogPostId)
        {
            var post = await _context.BlogPost.SingleOrDefaultAsync(bp => bp.BlogPostId == blogPostId);
           var comments=post.Comments.ToList();
               // await _context.BlogPost.Include(bp => bp.Comments)
               // .SingleOrDefaultAsync(bp => bp.BlogPostId == blogPostId);
            return comments;
        }
        public async Task<BlogPost> GetById(int blogPostId)
        {
            var blogPost = await _context.BlogPost
                .Include(bp => bp.Comments)
                .Include(p => p.PostRatings)
                .SingleOrDefaultAsync(bp => bp.BlogPostId == blogPostId);
            return blogPost;
        }
        public void Remove(BlogPost blogPost)
        {
            _context.BlogPost.Remove(blogPost);
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(BlogPost blogPost)
        {
            _context.Update(blogPost);
        }


    }
}
