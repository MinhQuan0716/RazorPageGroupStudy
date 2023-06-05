using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IGroupService
    {
        Task<List<Group>> GetListAsync();
        Task<Group> GetByIdAsync(int id);
        Task UpdateAsync(Group group);
        Task DeleteAsync(int id);
        Task CreateAsync(Group group);
        Task CreateListAsync(List<Group> groupList);
    }
}
