using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.IRepositories;
using Application.IService;
using Domain.Entities;

namespace Application.Service
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepo _commentRepo;
        private readonly IUnitOfWork _unitOfWork;
        public CommentService(ICommentRepo commentRepo, IUnitOfWork unitOfWork)
        {
            _commentRepo = commentRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Comment>> GetAllCommentByPostId(int postId)
        {
            return await _commentRepo.GetAllCommentByPostId(postId);
        }
    }
}
