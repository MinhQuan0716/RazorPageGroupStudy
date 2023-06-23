using Application.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracstuructures.Repositories
{
    public class PostRepo : GenericRepo<Post>, IPostRepo
    {
        private  readonly AppDbContext _appDbContext;
        public PostRepo(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public async Task<List<Post>> GetSearchPost(string content)
        {
            var searchPost=await _appDbContext.Posts.Where(x=>x.Content.Contains(content)).ToListAsync();
            return searchPost;
        }

        public async Task<List<Post>> SortPostByCreateDate()
        {
            var sortPost= await _appDbContext.Posts.OrderByDescending(x=>x.CreateTime).ToListAsync();
            return sortPost;
        }
        public  async Task<int> GetPostAmountInGroup(int groupId)
        {
            int postAmount=  _appDbContext.Posts.Include(x=>x.Group.Id==groupId).ToList().Count();
            return postAmount;
        }
    }
}
