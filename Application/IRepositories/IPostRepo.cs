using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IPostRepo : IGenericRepo<Post>
    {
        Task<List<Post>> GetSearchPost(string content);
        Task<List<Post>> SortPostByCreateDate(int groupId);
        Task<int> GetPostAmountInGroup(int groupId);
		Task<List<Post>> GetPostsByGroupId (int groupId);
        Task<List<Post>> GetPostsByUserId(int userId, int groupId);
        Task<List<Post>> GetPostsPendingByUserId(int userId, int groupId);
        Task<List<Post>> GetPostsBannedByUserId(int userId, int groupId);
    }
}
