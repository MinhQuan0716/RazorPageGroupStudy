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
        public async Task<IActionResult> OnPost()
        {
            // Handle the form submission here
            // You can access the submitted values using the 'Name' and 'Email' properties
            // For example: string name = Name;
            //              string email = Email;
            if (Email == null || Password == null)
            {
                ErrorMessage = "Email or password is null";
                return Page();
            }
            User loginUser = await _userService.LoginAsync(Email, Password);
            bool isExisted = await _userService.CheckEmail(Email);
            if (loginUser == null && !isExisted)
            {
                ErrorMessage = "Email is incorrect";
                return Page();
            }
            else if (loginUser == null)
            {
                ErrorMessage = "Password is incorrect";
                return Page();
            }
            if (loginUser != null)
            {
                if (loginUser.RoleId == 2)
                {
                    string login = JsonConvert.SerializeObject(loginUser);
                    _contextAccessor.HttpContext.Session.SetString("User", login);
                    bool isLogin = true;
                    bool isAdmin = false;
                    _contextAccessor.HttpContext.Session.Set("isLogin", BitConverter.GetBytes(isLogin));
                    _contextAccessor.HttpContext.Session.Set("isAdmin", BitConverter.GetBytes(isAdmin));
                    return RedirectToPage("/Home");
                }
                else if (loginUser.RoleId == 1)
                {
                    bool isAdmin = true;
                    bool isLogin = false;
                    _contextAccessor.HttpContext.Session.Set("isAdmin", BitConverter.GetBytes(isAdmin));
                    _contextAccessor.HttpContext.Session.Set("isLogin", BitConverter.GetBytes(isLogin));
                    return RedirectToPage("/AdminPage/Index");
                }

            }
            return Page();
        }
        public void OnGet()
        {

        }
    }
}