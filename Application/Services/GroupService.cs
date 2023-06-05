using Application.IServices;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GroupService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreateAsync(Group group)
        {
            await _unitOfWork.GroupRepo.AddAsync(group);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CreateListAsync(List<Group> groupList)
        {
            await _unitOfWork.GroupRepo.AddRangeAsync(groupList);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var itemToDelete = await _unitOfWork.GroupRepo.GetByIdAsync(id);
            await _unitOfWork.GroupRepo.RemoveAsync(itemToDelete);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Group> GetByIdAsync(int id)
        {
            var item = await _unitOfWork.GroupRepo.GetByIdAsync(id);
            return item;
        }

        public async Task<List<Group>> GetListAsync()
        {
            var item = await _unitOfWork.GroupRepo.GetAllAsync();
            return item;
        }

        public async Task UpdateAsync(Group group)
        {
            _unitOfWork.GroupRepo.Update(group);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
