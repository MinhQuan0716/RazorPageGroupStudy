using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IUserRepo : IGenericRepo<User>
    {
        Task<bool> CheckEmailDuplicateAsync(string email);
        Task<User> GetUserAsync(string email,string password);
        Task<List<User>> GetUsersByGroupId(int groupId);
    }
}
