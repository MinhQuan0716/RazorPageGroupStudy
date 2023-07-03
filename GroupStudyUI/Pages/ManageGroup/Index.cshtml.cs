using System;
using System.Collections.Generic;
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
        public IndexModel(IUserService userService, IGroupService groupService, IPostService postService, IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _postService = postService;
            _groupService = groupService;
            _userService = userService;
        }
        [BindProperty]
        public int GroupId { get; set; }
        public List<User> listUserInGroup { get; set; }
        public List<Post> listPostInGroup { get; set; }
		[BindProperty]
		public Group groupInfo { get; set; }
        public async Task<IActionResult> OnGet(int groupId)
        {
            GroupId = groupId;
            listUserInGroup = await _userService.GetUsersByGroupId(groupId);
            listPostInGroup = await _postService.GetPostsByGroupId(groupId);
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

	}
}
