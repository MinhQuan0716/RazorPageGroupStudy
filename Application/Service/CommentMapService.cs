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
	public class CommentMapService : ICommentMapService
	{
		private readonly ICommentMapRepo _commentRepo;
		private readonly IUnitOfWork _unitOfWork;
		public CommentMapService(ICommentMapRepo commentRepo, IUnitOfWork unitOfWork)
		{
			_commentRepo = commentRepo;
			_unitOfWork = unitOfWork;
		}

		public async Task<bool> DeleteAllMappingComment(int subComment)
		{
		   await _commentRepo.RemoveAllCommentMapping(subComment);
			return await _unitOfWork.SaveChangesAsync()>0;
		}

		public async Task<bool> DeleteAllReplyComment(int parentComment)
		{
			await _commentRepo.DeleteChildComment(parentComment);
			return await _unitOfWork.SaveChangesAsync() > 0;
		}

		public async Task<List<CommentMap>> GetAllMappingComment(int subComment)
		{
			return await _commentRepo.GetAllCommentMapping(subComment);
		}

		public async	Task<List<CommentMap>> GetAllReplyComment(int parentComment)
		{
			return await _commentRepo.GetAllChildComment(parentComment);
		}
	}
}
