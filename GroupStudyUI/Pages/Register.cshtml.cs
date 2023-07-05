using Application.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace GroupStudyUI.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string Name { get; set; }    
        private readonly IUserService _userService;
        public RegisterModel (IUserService userService)
        {
            _userService = userService;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            try
            {
                var user = await _userService.RegisterAsync(Email, Password, Name);
            } catch(Exception ex)
            {
                ModelState.AddModelError(ex.Message,ex.Message);
                return Page();
            }
           
            return RedirectToPage("/Login");
        }
    }
}
