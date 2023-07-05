using Application.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infracstuructures.Repositories
{
	public class CommentRepo : GenericRepo<Comment>, ICommentRepo
    {
        private AppDbContext _appDbContext;
        public CommentRepo(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

		public async Task<List<Comment>> GetAllCommentByGroupId(int groupId)
		{
			var listCommentInGroup = await _appDbContext.Comments.Include(c => c.Post).ThenInclude(p => p.GroupId == groupId).ToListAsync();
			return listCommentInGroup;
		}

		public async Task<List<Comment>> GetAllCommentByPostId(int postId)
		{
			var listComment = await _appDbContext.Comments
				.Include(c => c.ParentComment) // Include child comments
				.Include(c => c.CommentUser) // Include comment user
				.Where(c => c.PostId == postId)
				.OrderByDescending(c => c.CreateDate)
				.ToListAsync();

			return listComment;
		}



	}
}
