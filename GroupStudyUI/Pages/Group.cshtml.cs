using Application.IService;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
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
        private readonly IUserGroupService _userGroupService;
        public GroupModel(IGroupService groupService, IPostService postService, IHttpContextAccessor contextAccessor,IUserGroupService userGroupService)
        {
            _groupService = groupService;
            _contextAccessor = contextAccessor;
            _postService = postService;
            _userGroupService = userGroupService;
        }
        [BindProperty]
        public Group Group { get; set; }
        public int UserRoleIdInGroup { get; set; }
        public bool CanManageGroup { get; set; } = false;
        public List<Post> listPosts { get; set; }
        [BindProperty]
        public List<Post> listPostsSortByDate { get; set; }
        [BindProperty]
        public List<Post> listPostsOfUser { get; set; }
        [BindProperty]
        public List<Post> listPostsPendingOfUser { get; set; }
        [BindProperty]
        public List<Post> listPostsBannedOfUser { get; set; }
        [BindProperty]
        public int TotalPostInGroup { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            string isLogin = HttpContext.Session.GetString("isLogin");

            if (isLogin == null || isLogin.Equals("false"))
            {
                return RedirectToPage("/Login");
            }


            Group = await _groupService.GetGroupBydId(id);

            if (Group == null)
            {
                return NotFound();
            }
            string customerJson = _contextAccessor.HttpContext.Session.GetString("User");
            var user = JsonConvert.DeserializeObject<User>(customerJson);

            UserRoleIdInGroup = await _groupService.GetUserRoleIdInGroup(user.Id, Group.Id);
            if (UserRoleIdInGroup == 1 || UserRoleIdInGroup == 2)
            {
                CanManageGroup = true;

            }

            listPostsPendingOfUser = await _postService.GetPostsPendingByUserId(user.Id, Group.Id);
            listPostsBannedOfUser = await _postService.GetPostsBannedByUserId(user.Id, Group.Id);

            listPostsOfUser = await _postService.GetPostsByUserId(user.Id, Group.Id);
            listPosts = await _postService.GetPostsByGroupId(Group.Id);
            listPostsSortByDate = await _postService.SortPostByNewestDay(Group.Id);
            TotalPostInGroup = await _postService.PostAmountInGroup(Group.Id);

            return Page();
        }
        public async Task<IActionResult> OnPostReportPost(int? postId)
        {
            if (postId == null)
            {
                return NotFound();
            }
            var reportPost = await _postService.GetPostById(postId.Value);
            if(reportPost == null)
            {
                return NotFound();
            }
            reportPost.PostStatusId = 4;
            await _postService.UpdatePost(reportPost);
            return Page();
        }
        public async Task <IActionResult> OnPostOutGroup(int? groupId)
        {
            if (groupId == null)
            {
                return NotFound();
            }
            var userLoginJson = _contextAccessor.HttpContext.Session.GetString("User");
            var userLoginData = JsonConvert.DeserializeObject<User>(userLoginJson);
            bool isOut = await _userGroupService.OutGroup(userLoginData.Id, groupId.Value);
            if (!isOut)
            {
                return Page();
            }
            var groupInfo =await _groupService.GetGroupBydId(groupId.Value);
            groupInfo.memberAmount--;
            await _groupService.UpdateGroup(groupInfo);
            return RedirectToPage("/Explore");
        }
    }
}