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
        private readonly IUnitOfWork _unitOfWork;
        public PostService(IPostRepo postRepo,IUnitOfWork unitOfWork)
        {
            _postRepo = postRepo;
            _unitOfWork = unitOfWork;
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

        public async Task<List<Post>> SortPostByNewestDay(int groupId)
        {
            var sortPostList = await _postRepo.SortPostByCreateDate(groupId);
            return sortPostList;
        }

        public async Task<List<Post>> GetPostsByGroupId(int groupId)
        {
            var listPost = await _postRepo.GetPostsByGroupId(groupId);
            return listPost;
        }

        public async Task<bool> CreatePost(string title, string content, int groupId, int userId, string fileUrl)
        {
            var post = new Post
            {
                PostTitle = title,
                Content = content,
                GroupId = groupId,
                CreateUserId = userId,
                CreateTime = DateTime.Now,
                CommentOnPost = 0,
                PostStatusId = 1,
                fileURL = fileUrl
            };
           await _postRepo.AddAsync(post);
            return await _unitOfWork.SaveChangesAsync()>0;
        }

        public async Task<bool> DeletePost(int postId)
        {
            var post = await _postRepo.GetByIdAsync(postId);
            if(post != null)
            {
                await _postRepo.RemoveAsync(post);
            }
            else
            {
                throw new Exception("Post do not exist");
            }
            return await _unitOfWork.SaveChangesAsync()>0;
        }

        public async Task<Post> GetPostById(int postId)
        {
            var post = await _postRepo.GetByIdAsync(postId);
            return post;
        }

        public async Task<List<Post>> GetPostsByUserId(int userId, int groupId)
        {
            var listPost = await _postRepo.GetPostsByUserId(userId, groupId);
            return listPost;
        }

        public async Task<List<Post>> GetPostsPendingByUserId(int userId, int groupId)
        {
            var listPost = await _postRepo.GetPostsPendingByUserId(userId, groupId);
            return listPost;
        }
        public async Task<List<Post>> GetPostsBannedByUserId(int userId, int groupId)
        {
            var listPost = await _postRepo.GetPostsBannedByUserId(userId, groupId);
            return listPost;
        }
    }
}
