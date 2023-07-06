using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infracstuructures.Repositories
{
	public class CommentMapRepo : GenericRepo<CommentMap>, ICommentMapRepo
	{
		private AppDbContext _appDbContext;
		public CommentMapRepo(AppDbContext context) : base(context)
		{
			_appDbContext = context;
		}

		public async Task DeleteChildComment(int parentComment)
		{
			var listChildComment= await _appDbContext.CommentMap.Where(c=>c.ParentCommentId==parentComment).ToListAsync();
			foreach (var comment in listChildComment)
			{
				var childComment =await  _appDbContext.Comments.SingleOrDefaultAsync(co => co.Id == comment.SubCommentId);
				_appDbContext.Comments.Remove(childComment);
				_appDbContext.CommentMap.Remove(comment);
			}
		}

		public async Task<List<CommentMap>> GetAllChildComment(int parentComment)
		{
			return  await _appDbContext.CommentMap.Where(c => c.ParentCommentId == parentComment).ToListAsync();
		}

		public async Task<List<CommentMap>> GetAllCommentMapping(int subComment)
		{
			return await _appDbContext.CommentMap.Where(x=>x.SubCommentId==subComment).ToListAsync();	
		}

		public async Task RemoveAllCommentMapping(int subComment)
		{
			var listCommentMapping=await GetAllCommentMapping(subComment);
			foreach (var comment in listCommentMapping)
			{
				_appDbContext.CommentMap.Remove(comment);
			}
		}
	}
}
