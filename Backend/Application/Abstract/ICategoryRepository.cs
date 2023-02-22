using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface ICategoryRepository
    {
        Task<Category> GetById(int categoryId);
        Task Add(Category category);
        Task Save();
        void Remove(Category category);
        Task<List<Category>> GetAll();
        Task Update(Category category);
    }
}
