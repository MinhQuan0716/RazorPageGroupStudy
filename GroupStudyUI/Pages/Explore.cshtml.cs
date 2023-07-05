using System.Collections.Generic;
using System.Threading.Tasks;
using Application.IService;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace GroupStudyUI.Pages
{
    public class ExploreModel : PageModel
    {
		private readonly IGroupService _groupService;
		private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserGroupService _userGroupService;
		public ExploreModel(IGroupService groupService, IHttpContextAccessor contextAccessor,IUserGroupService userGroupService)
		{
			_groupService = groupService;
			_contextAccessor = contextAccessor;
            _userGroupService = userGroupService;
		}
        public List<Group> listGroups { get; set; }
        [BindProperty]
        public string searchName { get; set; }
        public async Task OnGet()
        {
            IEnumerable<Group> groups = await _groupService.GetAllGroupV2();
            listGroups = new List<Group>(groups);
        }
        public async Task<IActionResult> OnPostSearch() 
        {
            if (searchName == null)
            {
                IEnumerable<Group> group = await _groupService.GetAllGroupV2();
                listGroups = new List<Group>(group);
               return Page();  
            }
            List<Group> groups = new List<Group>();
            groups=await _groupService.SearchGroupByName(searchName);
            listGroups=groups;
            return Page();
        }
        public async Task<IActionResult> OnPostJoinGroup(int? groupId)
        {
           
            var userLoginJson = _contextAccessor.HttpContext.Session.GetString("User");
            var userLoginData = JsonConvert.DeserializeObject<User>(userLoginJson);
            var isInGroup = await _userGroupService.CheckUserExisted(userLoginData.Id, groupId.Value);
            if (isInGroup)
            {
                return RedirectToPage("/Group", new { id = groupId });
            }
            if (groupId == null)
            {
                return NotFound();
            }
            var joinedGroup = await _groupService.GetGroupBydId(groupId.Value);
            if(joinedGroup == null)
            {
                return NotFound();
            }

            joinedGroup.memberAmount++;
          await  _groupService.UpdateGroup(joinedGroup);
            var userJoinGroup = new UserGroup
            {
                GroupId=groupId.Value,
                UserId=userLoginData.Id,
                GroupRoleId=3,
                isBanned=false,
            };
            await _userGroupService.AddUserToGroup(userJoinGroup);  
            return RedirectToPage("/Group",new {id=groupId});
        }
    }
}
