using Application.Abstract;
using DataAccess.Repositories;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public UnitOfWork(DataContext context, IBlogRepository blogRepository, IBlogPostRepository blogPostRepository,
            IUserRepository userRepository, ICategoryRepository categoryRepository)
        {
            _context = context;
            BlogRepository = blogRepository;
            BlogPostRepository = blogPostRepository;
            UserRepository = userRepository;
            CategoryRepository= categoryRepository;
        }

        public IBlogRepository BlogRepository { get; private set; }
        public IBlogPostRepository BlogPostRepository { get; private set; }
        public IUserRepository UserRepository { get; private set; }
        public ICategoryRepository CategoryRepository { get; private set; }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
