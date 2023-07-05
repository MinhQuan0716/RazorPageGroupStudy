using Application.IService;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GroupStudyUI.Pages
{
    public class InformationModel : PageModel
    {
        [BindProperty]
        public User infoUser { get; set; }
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;
        public InformationModel (IUserService userService,IHttpContextAccessor contextAccessor)
        {
            _userService = userService;
            _contextAccessor = contextAccessor;
        }
        public IActionResult OnGet()
        {
            string isLogin = HttpContext.Session.GetString("isLogin");

            if (isLogin == null || isLogin.Equals("false"))
            {
                return RedirectToPage("/Login");
            }

            string customerJson = _contextAccessor.HttpContext.Session.GetString("User");
			var user = JsonConvert.DeserializeObject<User>(customerJson);
            infoUser = user;
			return Page();

        }
  
    }
}
