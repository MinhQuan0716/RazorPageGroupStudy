using Application.IService;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace GroupStudyUI.Pages.AdminPage
{
    public class BanUserModel : PageModel
    {
        [BindProperty]
        public User User { get; set; }
        private readonly IUserService _userService;
        public BanUserModel(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> OnGet (int? id)
        {
			string isLogin = HttpContext.Session.GetString("isLogin");
			string isAdmin = HttpContext.Session.GetString("isAdmin");

			if (isLogin == null || isLogin.Equals("false") || isAdmin.Equals("false"))
			{
				return RedirectToPage("/Login");
			}

			if (id == null)
            {
                return NotFound();
            }
          User=await _userService.FindUserById(id.Value);
            if(User == null)
            {
                return NotFound();
            }
            return Page();
            
        }
        public async Task<IActionResult> OnPost (int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
          User=await _userService.FindUserById(id.Value);
            if(User != null )
            {
             await  _userService.SoftRemove(id.Value);
            }
            return RedirectToPage("/AdminPage/Index");

        }
    }
}
