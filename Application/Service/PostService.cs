using Application.IRepositories;
using Application.IService;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class PostService : IPostService
    {
        private readonly IPostRepo _postRepo;
        public PostService(IPostRepo postRepo)
        {
            _postRepo = postRepo;
        }

        public async Task<int> PostAmountInGroup(int groupId)
        {
            return await _postRepo.GetPostAmountInGroup(groupId);
        }

        public async Task<List<Post>> SearchPost(string content)
        {
            var postList=await _postRepo.GetSearchPost(content);
            return postList;
        }

        public async Task<List<Post>> SortPostByNewestDay()
        {
            var sortPostList = await _postRepo.SortPostByCreateDate();
            return sortPostList;
        }

    }
}
