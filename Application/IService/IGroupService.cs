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
        Task<bool> CreateGroup(Group group);
		Task UpdateGroup(Group group);
		Task<List<Group>> GetJoinedGroup(int userId);
		Task<List<Group>> GetAdminGroup(int userId);
		Task<List<Group>> GetModeratorGroup(int userId);
		Task<List<Group>> SearchGroupByName(string name);
        Task<List<Group>> GetAllGroupV2();
        Task<Group> GetGroupBydId(int groupId);
        Task<int> GetUserRoleIdInGroup(int userId, int groupId);
        Task<Group> GetSavedGroup();
        Task SoftRemoveGroup(int groupId);
        Task<List<Group>> GetAllGroupV3();
        Task<Group> GetGroupByLink(string inviteUrl);
        Task<bool> RemoveGroup (int groupId);   
        Task<bool> CheckInviteUrlExisted(string inviteUrl); 
    }
}
