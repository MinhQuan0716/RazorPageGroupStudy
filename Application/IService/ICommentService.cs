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
        //Task<List<Comment>> GetCommentParentOfPost(int postId);
    }
}
