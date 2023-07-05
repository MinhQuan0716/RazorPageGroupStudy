using Application.IService;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupStudyUI.Pages.UserManagement
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<User> Users { get; set; }
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;
        public IndexModel(IUserService userService,IHttpContextAccessor contextAccessor)
        {
            _userService = userService;
            _contextAccessor = contextAccessor;
        }
        public async Task<IActionResult> OnGet()
        {
            if(!_contextAccessor.HttpContext.Session.Keys.Any())
            {
                return RedirectToPage("/Login");
            }
            bool isAdmin = BitConverter.ToBoolean(_contextAccessor.HttpContext.Session.Get("isAdmin"));
            if(!isAdmin)
            {
                return RedirectToPage("/Login");
            }
            Users=await _userService.GetAllUserV2();
            return Page();
        }
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("User");
            HttpContext.Session.Remove("isAdmin");
            return RedirectToPage("/Login");
        }

    }
}
