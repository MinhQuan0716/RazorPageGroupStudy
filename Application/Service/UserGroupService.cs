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
    public class UserGroupService : IUserGroupService
    {
        private readonly IUserGroupRepo _userGroupRepo;
        private readonly IUnitOfWork _unitOfWork;
        public UserGroupService(IUserGroupRepo userGroupRepo,IUnitOfWork unitOfWork)
        {
            _userGroupRepo= userGroupRepo;
            _unitOfWork= unitOfWork;
        }
        public async Task AddUserToGroup(UserGroup userInGroup)
        {
            var userGroup =await _userGroupRepo.GetById(userInGroup.UserId,userInGroup.GroupId);
            if(userGroup == null)
            {
             await   _userGroupRepo.AddAsync(userInGroup);
            }
            await _unitOfWork.SaveChangesAsync();
        }

		public async Task<bool> BanUserFromGroup(int userId)
		{
            var userGroup = await _userGroupRepo.isUserExisted(userId);
            if (userGroup != null)
            {
               /* userGroup.isBanned = true;
                userGroup.BannedDate=DateTime.Now;*/
                await  _userGroupRepo.RemoveAsync(userGroup);
            }
          return  await _unitOfWork.SaveChangesAsync()>0;
		}

		public async Task<bool> PromoteUser(int userId)
		{
			var userGroup = await _userGroupRepo.isUserExisted(userId);
			if (userGroup != null)
			{
                userGroup.GroupRoleId = 2;
				_userGroupRepo.Update(userGroup);
			}
			return await _unitOfWork.SaveChangesAsync() > 0;
		}
        public async Task<bool> CheckUserExisted(int userId,int gropuId) 
        {
            bool isExisted = false;
             var userGroup = await _userGroupRepo.GetById(userId,gropuId);
            if (userGroup != null)
            {
                isExisted = true;
            }
            return isExisted;
        }

        public async Task<UserGroup> FindUserInGroup(int userId, int groupId)
        {
            return await _userGroupRepo.GetById(userId,groupId);    
        }
        public async Task<bool> OutGroup(int userId,int groupId)
        {
            var userInGroup= await _userGroupRepo.GetById(userId,groupId);
            if (userInGroup != null)
            {
                await _userGroupRepo.RemoveAsync(userInGroup);
            }
            return await _unitOfWork.SaveChangesAsync()>0;
        }
    }
}
