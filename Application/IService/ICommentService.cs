using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.IService
{
    public interface ICommentService
    {
        Task<List<Comment>> GetAllCommentByPostId(int postId);
		Task<bool> CreateComment(int userId, int postId, string content);
		Task<bool> ReplyComment(int userId, int postId, string content, int replyCommentId);
		Task<List<Comment>> GetAllCommentByGroupId(int groupId);
		Task<bool> DeleteComment(int commentId);
		Task<Comment> GetCommentById(int commentId);
	}
}
