using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using System;
using System.Linq;

namespace GroupStudyUI.Pages
{
    public class HomeModel : PageModel
    {
        private readonly IHttpContextAccessor _contextAccessor; 
        public HomeModel(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public IActionResult OnGet()
        {
            if (!_contextAccessor.HttpContext.Session.Keys.Any())
            {
                return RedirectToPage("/login");
            }
            bool isLogin = BitConverter.ToBoolean(_contextAccessor.HttpContext.Session.Get("isLogin"));
            if (!isLogin)
            {
                return RedirectToPage("/Login");
            }
            return Page();
        }
    }
}
