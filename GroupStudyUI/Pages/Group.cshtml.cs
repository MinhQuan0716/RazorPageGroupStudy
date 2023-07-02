using Application.IService;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupStudyUI.Pages
{
    public class GroupModel : PageModel
    {
		private readonly IGroupService _groupService;
		private readonly IPostService _postService;
		private readonly IHttpContextAccessor _contextAccessor;
		public GroupModel(IGroupService groupService, IPostService postService, IHttpContextAccessor contextAccessor)
		{
			_groupService = groupService;
			_contextAccessor = contextAccessor;
			_postService = postService;
		}
		public Group Group { get; set; }
		public List<Post> listPosts { get; set; }
        public List<Post> listPostsSortByDate { get; set; }
		public List<Post> listPostsOfUser { get; set; }
        public List<Post> listPostsPendingOfUser { get; set; }
        public List<Post> listPostsBannedOfUser { get; set; }
        public int TotalPostInGroup { get; set; }
		public async Task<IActionResult> OnGet(int id)
		{
			Group = await _groupService.GetGroupBydId(id);
			

			if (Group == null)
			{
				return NotFound();
			}
            string customerJson = _contextAccessor.HttpContext.Session.GetString("User");
            var user = JsonConvert.DeserializeObject<User>(customerJson);

			listPostsPendingOfUser = await _postService.GetPostsPendingByUserId(user.Id, Group.Id);
            listPostsBannedOfUser = await _postService.GetPostsBannedByUserId(user.Id, Group.Id);

            listPostsOfUser = await _postService.GetPostsByUserId(user.Id, Group.Id);
            listPosts = await _postService.GetPostsByGroupId(Group.Id);
            listPostsSortByDate = await _postService.SortPostByNewestDay(Group.Id);
            TotalPostInGroup = await _postService.PostAmountInGroup(Group.Id);

			return Page();
		}

	}
}
