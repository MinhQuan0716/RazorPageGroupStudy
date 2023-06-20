using Application.IRepositories;
using Application.IService;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GroupStudyUI.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;
        public LoginModel(IUserService userService,IHttpContextAccessor contextAccessor)
        {
           _userService= userService;
            _contextAccessor= contextAccessor;
        }

         [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public async Task<IActionResult> OnPost()
        {
            // Handle the form submission here
            // You can access the submitted values using the 'Name' and 'Email' properties
            // For example: string name = Name;
            //              string email = Email;

           User loginUser= await _userService.LoginAsync(Email, Password);
            if (loginUser != null)
            {
                string login = JsonConvert.SerializeObject(loginUser);
                _contextAccessor.HttpContext.Session.SetString("User", login);
                return RedirectToPage("/Home");
            }
            
          
            return Page();
        }
        public void OnGet()
        {

        }
    }
}