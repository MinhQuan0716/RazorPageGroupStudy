using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.IRepositories;
using Domain.Entities;

namespace Infracstuructures.Repositories
{
	public class CommentMapRepo : GenericRepo<CommentMap>, ICommentMapRepo
	{
		private AppDbContext _appDbContext;
		public CommentMapRepo(AppDbContext context) : base(context)
		{
			_appDbContext = context;
		}
	}
}
