using Application.IService;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupStudyUI.Pages.UserManagement
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<User> Users { get; set; }
        private readonly IUserService _userService;
        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }
        public async Task OnGet()
        {
            Users=await _userService.GetAllUser();
        }
        public IActionResult OnPostLogout()
        {
            return RedirectToPage("/Login");
        }

    }
}
