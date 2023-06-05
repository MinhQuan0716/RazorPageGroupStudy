using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IUserGroupService
    {
        Task<List<UserGroup>> GetListAsync();
        Task<UserGroup> GetByIdAsync(int id);
        Task UpdateAsync(UserGroup userGroup);
        Task DeleteAsync(int id);
        Task CreateAsync(UserGroup userGroup);
        Task CreateListAsync(List<UserGroup> userGroupList);
    }
}
