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
        Task<List<Post>> SortPostByCreateDate();
        Task<int> GetPostAmountInGroup(int groupId);    
    }
}
