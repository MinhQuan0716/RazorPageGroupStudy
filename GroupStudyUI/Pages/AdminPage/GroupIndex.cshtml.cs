using Application.IService;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupStudyUI.Pages.AdminPage
{
    public class GroupIndexModel : PageModel
    {
        [BindProperty]
        public List<Group> Groups { get; set; }
        private readonly IGroupService _groupService;
        private readonly IHttpContextAccessor _contextAccessor;
        public GroupIndexModel(IGroupService groupService,IHttpContextAccessor contextAccessor)
        {
            _groupService = groupService;
            _contextAccessor = contextAccessor;
        }
        public async Task<IActionResult> OnGet()
        {
			string isLogin = HttpContext.Session.GetString("isLogin");
			string isAdmin = HttpContext.Session.GetString("isAdmin");

			if (isLogin == null || isLogin.Equals("false") || isAdmin.Equals("false"))
			{
				return RedirectToPage("/Login");
			}

			Groups = await  _groupService.GetAllGroupV3();
            return Page();
        }
        public IActionResult OnPostLogout()
        {
            return RedirectToPage("/Login");
        }
    }
}
