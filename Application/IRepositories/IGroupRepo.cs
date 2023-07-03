using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IGroupRepo : IGenericRepo<Group>
    {
		Task UpdateGroup(Group group);
		Task<List<Group>> GetJoinedGroup(int userId);
		Task<List<Group>> GetAdminGroup(int userId);
		Task<List<Group>> GetModeratorGroup(int userId);
		Task<List<Group>> GetSearchGroup(string groupName);
        Task<List<Group>> GetAllGroupV2();
        Task<int> GetUserRoleIdInGroup(int userId, int groupId);
    }
}
