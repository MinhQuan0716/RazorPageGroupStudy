using Application.IService;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Linq;
using System;

namespace GroupStudyUI.Pages
{
    public class YourGroupsModel : PageModel
    {
        private readonly IGroupService _groupService;
        private readonly IHttpContextAccessor _contextAccessor;
        public YourGroupsModel(IGroupService groupService, IHttpContextAccessor contextAccessor)
        {
            _groupService = groupService;
            _contextAccessor = contextAccessor;
        }
        public List<Group> listGroups { get; set; }
        public List<Group> listAdminGroups { get; set; }
        public List<Group> listModeratorGroups { get; set; }
        public async Task<IActionResult> OnGet()
        {
            string isLogin = HttpContext.Session.GetString("isLogin");

            if (isLogin == null || isLogin.Equals("false"))
            {
                return RedirectToPage("/Login");
            }

            string customerJson = _contextAccessor.HttpContext.Session.GetString("User");
            var user = JsonConvert.DeserializeObject<User>(customerJson);

            listGroups = await _groupService.GetJoinedGroup(user.Id);
            listAdminGroups = await _groupService.GetAdminGroup(user.Id);
            listModeratorGroups = await _groupService.GetModeratorGroup(user.Id);

            return Page();

        }
    }
}
