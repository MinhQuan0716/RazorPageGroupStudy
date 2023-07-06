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
				_appDbContext.CommentMap.Remove(comment);
			}
		}

		public async Task<List<CommentMap>> GetAllChildComment(int parentComment)
		{
			return  await _appDbContext.CommentMap.Where(c => c.ParentCommentId == parentComment).ToListAsync();
		}
	}
}
