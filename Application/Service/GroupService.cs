using Application.IRepositories;
using Application.IService;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepo _groupRepo;
        private readonly IUnitOfWork _unitOfWork;
        public GroupService(IGroupRepo groupRepo,IUnitOfWork unitOfWork)
        {
            _groupRepo = groupRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateGroup(Group group)
        {
            var isExistedGroup= await _groupRepo.GetByIdAsync(group.Id);
            if(isExistedGroup == null) 
            {
                Group newGroup = new Group()
                {
                    Name = group.Name,
                    Description = group.Description,
                    CreateDate = DateTime.UtcNow,
                    Status = group.Status,
                    InviteUrl = group.InviteUrl,
                    memberAmount=group.memberAmount
                };
                await _groupRepo.AddAsync(newGroup);
            }
           
            return await _unitOfWork.SaveChangesAsync()>0;
        }
		public async Task UpdateGroup(Group group)
		{
			await _groupRepo.UpdateGroup(group);
		}

		public async Task<IEnumerable<Group>> GetAllGroup()
        {
            return await _groupRepo.GetAllAsync();
        }

        public async Task<List<Group>> GetAllGroupV2()
        {
            return await _groupRepo.GetAllGroupV2();
        }

        public async Task<List<Group>> GetJoinedGroup(int userId)
        {
            return await _groupRepo.GetJoinedGroup(userId);
        }

        public async Task<List<Group>> SearchGroupByName(string name)
        {
            return await _groupRepo.GetSearchGroup(name); 
        }

        public async Task<Group> GetGroupBydId(int groupId)
        {
            return await _groupRepo.GetByIdAsync(groupId);
        }
		public async Task<List<Group>> GetAdminGroup(int userId)
		{
			return await _groupRepo.GetAdminGroup(userId);
		}
		public async Task<List<Group>> GetModeratorGroup(int userId)
		{
			return await _groupRepo.GetModeratorGroup(userId);
		}
        public async Task<int> GetUserRoleIdInGroup(int userId, int groupId)
        {
            return await _groupRepo.GetUserRoleIdInGroup(userId, groupId);
        }

        public async Task<Group> GetSavedGroup()
        {
            return await _groupRepo.GetLastSavedGroup();
        }

        public async Task SoftRemoveGroup(int groupId)
        {
            var group =await _groupRepo.GetByIdAsync(groupId);
            if(group!=null) 
            {
                group.isDeleted = true;
                await _groupRepo.UpdateGroup(group);
            }
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<Group>> GetAllGroupV3()
        {
            return await _groupRepo.GetAllGroupV3();
        }

		public async Task<Group> GetGroupByLink(string inviteUrl)
		{
            return await _groupRepo.GetGroupThroughLink(inviteUrl);
		}

        public async Task<bool> RemoveGroup(int groupId)
        {
            var groupInfo= await _groupRepo.GetByIdAsync(groupId);
            if(groupInfo!=null)
            {
                await _groupRepo.RemoveAsync(groupInfo);
            }
            return await _unitOfWork.SaveChangesAsync()>0;
        }

		public async Task<bool> CheckInviteUrlExisted(string inviteUrl)
		{
            bool isExisted = false;
            var group = await _groupRepo.GetGroupThroughLink(inviteUrl);
            if(group!=null )
            {
                isExisted = true;
            }
            return isExisted;
		}
	}
}
