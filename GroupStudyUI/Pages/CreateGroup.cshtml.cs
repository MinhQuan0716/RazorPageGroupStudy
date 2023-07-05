using Application.IService;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace GroupStudyUI.Pages
{
    public class CreateGroupModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public string Description { get; set; }
        [BindProperty]
        public string Status { get; set; }
        private readonly IGroupService _groupService;
        private readonly IUserGroupService _userGroupService;
        private readonly IHttpContextAccessor _contextAccessor;
        public CreateGroupModel (IGroupService groupService,IHttpContextAccessor contextAccessor,IUserGroupService userGroupService)
        {
            _groupService = groupService;
            _contextAccessor = contextAccessor;
            _userGroupService = userGroupService;
        }
        public IActionResult OnGet()
        {
            if (_contextAccessor.HttpContext.Session.Keys.Any())
            {
                return Page();
            }
            return RedirectToPage("/Login");
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return Page();
            }
            var userLoginJson = _contextAccessor.HttpContext.Session.GetString("User");
            var userLoginData = JsonConvert.DeserializeObject<User>(userLoginJson);
            Group newGroup = new Group
            {
                Name = Name,
                Description = Description,
                Status = Status,
                
            };
            newGroup.memberAmount = 0;
            ++newGroup.memberAmount ;
            await _groupService.CreateGroup(newGroup);
            Group isInDb = await _groupService.GetSavedGroup();
            if (isInDb!=null)
            {
                UserGroup userInGroup = new UserGroup
                {
                    GroupId = isInDb.Id,
                    UserId = userLoginData.Id,
                    CreatedDate = System.DateTime.Now,
                    GroupRoleId = 1,
                    
                };
                await _userGroupService.AddUserToGroup(userInGroup);
            }
           
            return RedirectToPage("/YourGroups");
        }
    }
}
