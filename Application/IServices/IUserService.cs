using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IUserService
    {

        Task<List<User>> GetListAsync();
        Task<User> GetByIdAsync(int id);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task CreateAsync(User user);
        Task CreateListAsync(List<User> userList);
    }
}
