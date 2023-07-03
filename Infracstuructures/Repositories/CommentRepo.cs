using Application.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infracstuructures.Repositories
{
    public class CommentRepo : GenericRepo<Comment>, ICommentRepo
    {
        private AppDbContext _appDbContext;
        public CommentRepo(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }
        public async Task<List<Comment>> GetAllCommentByPostId(int postId)
        {
            var listComment = await _appDbContext.Comments
                .Where(p => p.PostId == postId)
                .OrderByDescending(p => p.CreateDate)
                .ToListAsync();
            return listComment;
        }
    }
}
