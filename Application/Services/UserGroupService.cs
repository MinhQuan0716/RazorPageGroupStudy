using Application.IServices;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserGroupService : IUserGroupService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserGroupService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreateAsync(UserGroup userGroup)
        {
            await _unitOfWork.UserGroupRepo.AddAsync(userGroup);
        }

        public async Task CreateListAsync(List<UserGroup> userGroupList)
        {
            await _unitOfWork.UserGroupRepo.AddRangeAsync(userGroupList);
        }

        public async Task DeleteAsync(int id)
        {
            var itemToDelete = await _unitOfWork.UserGroupRepo.GetByIdAsync(id);
            await _unitOfWork.UserGroupRepo.RemoveAsync(itemToDelete);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<UserGroup> GetByIdAsync(int id)
        {
            var item = await _unitOfWork.UserGroupRepo.GetByIdAsync(id);
            return item;
        }

        public async Task<List<UserGroup>> GetListAsync()
        {
            var item = await _unitOfWork.UserGroupRepo.GetAllAsync();
            return item;
        }

        public async Task UpdateAsync(UserGroup userGroup)
        {
            _unitOfWork.UserGroupRepo.Update(userGroup);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
