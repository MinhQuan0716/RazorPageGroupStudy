using Application.IServices;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreateAsync(Post post)
        {
            await _unitOfWork.PostRepo.AddAsync(post);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CreateListAsync(List<Post> postList)
        {
            await _unitOfWork.PostRepo.AddRangeAsync(postList);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var itemToDelete = await _unitOfWork.PostRepo.GetByIdAsync(id);
            await _unitOfWork.PostRepo.RemoveAsync(itemToDelete);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            var item = await _unitOfWork.PostRepo.GetByIdAsync(id);
            return item;
        }

        public async Task<List<Post>> GetListAsync()
        {
            var item = await _unitOfWork.PostRepo.GetAllAsync();
            return item;
        }

        public async Task UpdateAsync(Post post)
        {
            _unitOfWork.PostRepo.Update(post);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
