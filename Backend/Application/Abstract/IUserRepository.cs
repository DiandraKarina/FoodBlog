using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IUserRepository
    {
        Task<User> GetById(int userId);
        Task Add(User user);
        Task Save();
        void Remove(User user);
        Task<List<User>> GetAll();
        Task Update(User user);
    }
}
