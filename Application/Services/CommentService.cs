using Application.IServices;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreateAsync(Comment comment)
        {
            await _unitOfWork.CommentRepo.AddAsync(comment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CreateListAsync(List<Comment> commentList)
        {
            await _unitOfWork.CommentRepo.AddRangeAsync(commentList);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var itemToDelete = await _unitOfWork.CommentRepo.GetByIdAsync(id);
            await _unitOfWork.CommentRepo.RemoveAsync(itemToDelete);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            var item = await _unitOfWork.CommentRepo.GetByIdAsync(id);
            return item;
        }

        public async Task<List<Comment>> GetListAsync()
        {
            var item = await _unitOfWork.CommentRepo.GetAllAsync();
            return item;
        }

        public async Task UpdateAsync(Comment comment)
        {
            _unitOfWork.CommentRepo.Update(comment);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
