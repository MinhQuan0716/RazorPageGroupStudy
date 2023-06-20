using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace GroupStudyUI.Pages
{
    public class LoginModel : PageModel
    {
      
        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public IActionResult OnPost()
        {
            // Handle the form submission here
            // You can access the submitted values using the 'Name' and 'Email' properties
            // For example: string name = Name;
            //              string email = Email;


            return Page();
        }
        public void OnGet()
        {

        }
    }
}