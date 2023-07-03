using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public  interface IPostService
    {
        Task<List<Post>> SearchPost(string content);
        Task<List<Post>> SortPostByNewestDay(int groupId);
        Task<int> PostAmountInGroup(int groupId);
		Task<List<Post>> GetPostsByGroupId(int groupId);
        Task<bool> CreatePost(string title, string content, int groupId, int userId);
        Task<bool> DeletePost(int postId);
        Task<Post> GetPostById(int postId);
        Task<List<Post>> GetPostsByUserId(int userId, int groupId);
        Task<List<Post>> GetPostsPendingByUserId(int userId, int groupId);
        Task<List<Post>> GetPostsBannedByUserId(int userId, int groupId);
    }
}
