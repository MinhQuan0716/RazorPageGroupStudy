using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IPostService
    {
        Task<List<Post>> GetListAsync();
        Task<Post> GetByIdAsync(int id);
        Task UpdateAsync(Post post);
        Task DeleteAsync(int id);
        Task CreateAsync(Post post);
        Task CreateListAsync(List<Post> postList);
    }
}
