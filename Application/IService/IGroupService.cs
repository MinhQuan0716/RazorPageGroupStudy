using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public  interface IGroupService
    {
        Task<IEnumerable<Group>> GetAllGroup();
        Task<bool> CreateGroup(string name,string description,string status,string inviteUrl);
        Task<List<Group>> GetJoinedGroup(int userId);
		Task<List<Group>> GetAdminGroup(int userId);
		Task<List<Group>> GetModeratorGroup(int userId);
		Task<List<Group>> SearchGroupByName(string name);
        Task<List<Group>> GetAllGroupV2();
        Task<Group> GetGroupBydId(int groupId);
    }
}
