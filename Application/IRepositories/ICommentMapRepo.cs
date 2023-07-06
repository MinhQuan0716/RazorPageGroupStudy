using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.IRepositories
{
	public interface ICommentMapRepo : IGenericRepo<CommentMap>
	{
		Task DeleteChildComment(int parentComment);
		Task<List<CommentMap>> GetAllChildComment(int parentComment);
		Task<List<CommentMap>> GetAllCommentMapping(int subComment);
		Task RemoveAllCommentMapping(int subComment);	
	}
}
