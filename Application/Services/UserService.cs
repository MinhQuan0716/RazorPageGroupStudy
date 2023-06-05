using Application.IServices;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreateAsync(User user)
        {
            await _unitOfWork.UserRepo.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CreateListAsync(List<User> userList)
        {
            await _unitOfWork.UserRepo.AddRangeAsync(userList);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var itemToDelete = await _unitOfWork.UserRepo.GetByIdAsync(id);
            await _unitOfWork.UserRepo.RemoveAsync(itemToDelete);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var item = await _unitOfWork.UserRepo.GetByIdAsync(id);
            return item;
        }

        public async Task<List<User>> GetListAsync()
        {
            var item = await _unitOfWork.UserRepo.GetAllAsync();
            return item;
        }

        public async Task UpdateAsync(User user)
        {
            _unitOfWork.UserRepo.Update(user);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
