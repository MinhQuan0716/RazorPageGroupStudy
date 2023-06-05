using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface ICommentService
    {
        Task<List<Comment>> GetListAsync();
        Task<Comment> GetByIdAsync(int id);
        Task UpdateAsync(Comment comment);
        Task DeleteAsync(int id);
        Task CreateAsync(Comment comment);
        Task CreateListAsync(List<Comment> commentList);
    
    }
}
