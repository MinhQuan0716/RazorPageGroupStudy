using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public  interface IUserService
    {
        Task<bool> RegisterAsync(string email, string password,string name);
        Task<User> LoginAsync(string email, string password);
        Task<List<User>> GetUsersByGroupId(int groupId);
        Task<bool> UpdateUser (User user);
        Task<List<User>> GetAllUser();
        Task<bool> SoftRemove(int userId);
        Task<User> FindUserById(int userId);
        Task<List<User>> GetAllUserV2();
        Task<User> GetUserById(int id);
        Task<User> GetUserWithRole(int id);
        Task<bool> CheckEmail(string email);
    }
}
