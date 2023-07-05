using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Application.IService;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GroupStudyUI.Pages.ManageGroup
{
    public class IndexModel : PageModel
    {
        private readonly IPostService _postService;
        private readonly IGroupService _groupService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;
		private readonly IUserGroupService _userGroupService;
		private readonly ICommentService _commentService;
        public IndexModel(IUserService userService, IGroupService groupService, IPostService postService, IHttpContextAccessor contextAccessor,IUserGroupService userGroupService, ICommentService commentService)
        {
            _contextAccessor = contextAccessor;
            _postService = postService;
            _groupService = groupService;
            _userService = userService;
			_userGroupService= userGroupService;
			_commentService = commentService;
        }
        [BindProperty]
        public int GroupId { get; set; }
		[BindProperty]
        public List<User> listUserInGroup { get; set; }
		[BindProperty]
        public List<Post> listPostInGroup { get; set; }
		[BindProperty]
		public List<Comment> listCommentInGroup { get; set; }
		[BindProperty]
		public Domain.Entities.Group groupInfo { get; set; }
        public async Task<IActionResult> OnGet(int groupId)
        {
            string isLogin = HttpContext.Session.GetString("isLogin");

            if (isLogin == null || isLogin.Equals("false"))
            {
                return RedirectToPage("/Login");
            }

            TempData["GroupId"] = groupId;
            GroupId = groupId;
            listUserInGroup = await _userService.GetUsersByGroupId(groupId);
            listPostInGroup = await _postService.GetPostsByGroupId(groupId);
			listCommentInGroup = await _commentService.GetAllCommentByGroupId(groupId);
			groupInfo = await _groupService.GetGroupBydId(groupId);
            return Page();
        }

		public async Task<IActionResult> OnPost()
		{
			if (!ModelState.IsValid)
			{
				// Handle validation errors
				return Page();
			}

			var existingGroup = await _groupService.GetGroupBydId(GroupId);

			if (existingGroup == null)
			{
				// Handle the case where the group with the given ID is not found
				return NotFound();
			}

			// Update the properties of the existing group object with the submitted form values
			existingGroup.Name = groupInfo.Name;
			existingGroup.Description = groupInfo.Description;
			existingGroup.Status = groupInfo.Status;
			existingGroup.InviteUrl = groupInfo.InviteUrl;
			existingGroup.UpdateDate = DateTime.Now;

			// Call the group service to update the group in the database
			await _groupService.UpdateGroup(existingGroup);

			return RedirectToPage("/ManageGroup/Index", new { groupId = GroupId });
		}
		public async Task< IActionResult> OnPostBanUser(int? userId)
		{
			int groupId = (int)TempData["GroupId"];

			if (userId == null)
			{
				return NotFound();
			}
			bool isBan=await _userGroupService.BanUserFromGroup(userId.Value);

			if (!isBan)
			{
				return BadRequest();
			}
			listUserInGroup = await _userService.GetUsersByGroupId(groupId);
			TempData["Message"] = "User successfully banned.";
			return Page();
		}
		public async Task<IActionResult> OnPostPromoteUser(int? userId)
		{
			int groupId = (int)TempData["GroupId"];
			if (userId == null)
			{
				return NotFound();
			}
			bool isBan = await _userGroupService.PromoteUser(userId.Value);

			if (!isBan)
			{
				return BadRequest();
			}
			listUserInGroup = await _userService.GetUsersByGroupId(groupId);
			TempData["Message"] = "User is successfully promote.";
			return Page();
		}
		public async Task<IActionResult> OnPostBanPost(int? postId)
		{
			int groupId = (int)TempData["GroupId"];
			if (postId == null)
			{
				return NotFound();
			}
		Post post=	await _postService.GetPostById(postId.Value);
			if(post == null)
			{
				return NotFound();
			}
			post.PostStatusId = 3;
			bool isBanned = await _postService.UpdatePost(post);
			if(!isBanned)
			{
				return BadRequest();
			}
			TempData["Message"] = "Post is successfully banned";
			listPostInGroup= await _postService.GetPostsByGroupId(groupId);
			return Page();
		}
		public async Task<IActionResult> OnPostApprovePost(int? postId)
		{
			int groupId = (int)TempData["GroupId"];
			if (postId == null)
			{
				return NotFound();
			}
			Post post = await _postService.GetPostById(postId.Value);
			if (post == null)
			{
				return NotFound();
			}
			post.PostStatusId = 1;
			bool isBanned = await _postService.UpdatePost(post);
			if (!isBanned)
			{
				return BadRequest();
			}
			TempData["Message"] = "Post is successfully approve";
			listPostInGroup = await _postService.GetPostsByGroupId(groupId);
			return Page();
		}
	}
}
