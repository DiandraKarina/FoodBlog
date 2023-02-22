using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IBlogRepository
    {
        Task<Blog> GetById(int blogId);
        Task Add(Blog blog);
        Task Save();
        void Remove(Blog blog);
        Task<List<Blog>> GetAll();
        Task Update(Blog blog);
    }
}
