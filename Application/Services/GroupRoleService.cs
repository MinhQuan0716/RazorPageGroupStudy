using Application.IServices;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GroupRoleService : IGroupRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GroupRoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreateAsync(GroupRole groupRole)
        {
            await _unitOfWork.GroupRoleRepo.AddAsync(groupRole);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CreateListAsync(List<GroupRole> groupRoleList)
        {
            await _unitOfWork.GroupRoleRepo.AddRangeAsync(groupRoleList);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var itemToDelete = await _unitOfWork.GroupRoleRepo.GetByIdAsync(id);
            await _unitOfWork.GroupRoleRepo.RemoveAsync(itemToDelete);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<GroupRole> GetByIdAsync(int id)
        {
            var item = await _unitOfWork.GroupRoleRepo.GetByIdAsync(id);
            return item;
        }

        public async Task<List<GroupRole>> GetListAsync()
        {
            var item = await _unitOfWork.GroupRoleRepo.GetAllAsync();
            return item;
        }

        public async Task UpdateAsync(GroupRole groupRole)
        {
            _unitOfWork.GroupRoleRepo.Update(groupRole);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
