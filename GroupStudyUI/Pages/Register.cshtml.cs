using Application.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
            var user=await _userService.RegisterAsync(Email,Password,Name);
            if (!user)
            {
                ModelState.AddModelError(string.Empty, "Registration failed. Please try again later.");
                return Page();
            }
            return RedirectToPage("/Login");
        }
    }
}
