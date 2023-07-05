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
            if (!_contextAccessor.HttpContext.Session.Keys.Any())
            {
                return RedirectToPage("/Login");
            }
            bool isAdmin = BitConverter.ToBoolean(_contextAccessor.HttpContext.Session.Get("isAdmin"));
            if (!isAdmin)
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
