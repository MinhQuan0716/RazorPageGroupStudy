using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IGroupRoleService
    {
        Task<List<GroupRole>> GetListAsync();
        Task<GroupRole> GetByIdAsync(int id);
        Task UpdateAsync(GroupRole groupRole);
        Task DeleteAsync(int id);
        Task CreateAsync(GroupRole groupRole);
        Task CreateListAsync(List<GroupRole> groupRoleList);
    }
}
