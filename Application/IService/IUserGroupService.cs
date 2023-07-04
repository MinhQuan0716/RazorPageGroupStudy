using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public  interface IUserGroupService
    {
        Task AddUserToGroup(UserGroup userGroup);
        Task<bool> BanUserFromGroup(int userId);
        Task<bool> PromoteUser(int userId);
    }
}
