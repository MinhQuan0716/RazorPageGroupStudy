using Application.IRepositories;
using Application.IService;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GroupStudyUI.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;
        public LoginModel(IUserService userService, IHttpContextAccessor contextAccessor)
        {
            _userService = userService;
            _contextAccessor = contextAccessor;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string ErrorMessage { get; set; }
		public IActionResult OnGet()
		{
			// Clearing all session values
			_contextAccessor.HttpContext.Session.Clear();
            return Page();
		}

		public async Task<IActionResult> OnPost()
        {
 
            if (Email == null || Password == null)
            {
                ErrorMessage = "Email and password is required";
                return Page();
            }

            User loginUser = await _userService.LoginAsync(Email, Password);
            bool isExisted = await _userService.CheckEmail(Email);

            if (!isExisted)
            {
                ErrorMessage = "Email is not existed";
                return Page();
            }
            else if (loginUser == null)
            {
                ErrorMessage = "Password is incorrect";
                return Page();
            } else if (loginUser.IsDeleted == true)
            {
                ErrorMessage = "Your account has been deleted";
                return Page();
            }

            if (loginUser != null)
            {
                string login = JsonConvert.SerializeObject(loginUser);
                _contextAccessor.HttpContext.Session.SetString("User", login);

                if (loginUser.RoleId == 2)
                {
					_contextAccessor.HttpContext.Session.SetString("isLogin", "true");
					_contextAccessor.HttpContext.Session.SetString("isAdmin", "false");
                    
                    return RedirectToPage("/Home");
                }
                else if (loginUser.RoleId == 1)
                {
					_contextAccessor.HttpContext.Session.SetString("isLogin", "true");
					_contextAccessor.HttpContext.Session.SetString("isAdmin", "true");

					return RedirectToPage("/AdminPage/Index");
                }

            }
            return Page();
        }

    }
}