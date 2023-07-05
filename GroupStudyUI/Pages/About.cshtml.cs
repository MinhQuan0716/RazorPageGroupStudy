using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GroupStudyUI.Pages
{
    public class AboutModel : PageModel
    {
        public IActionResult OnGet()
        {
            string isLogin = HttpContext.Session.GetString("isLogin");

            if (isLogin == null || isLogin.Equals("false"))
            {
                return RedirectToPage("/Login");
            }
            return Page();
        }
    }
}
