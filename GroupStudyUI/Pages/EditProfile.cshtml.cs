using Application.IService;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GroupStudyUI.Pages
{
    public class EditProfileModel : PageModel
    {
        [BindProperty]
        public User Users { get; set; }
        private readonly IUserService _userService;
        public EditProfileModel(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Users = await _userService.GetUserWithRole(id.Value);
            if(Users == null)
            {
                return NotFound();  
            }
            return Page();
        }
        public async Task<IActionResult> OnPost(int? userId)
        {
            Users= await _userService.GetUserWithRole(userId.Value);
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return Page();
            }

            try
            {
             await   _userService.UpdateUser(Users);
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;

            }

            return RedirectToPage("/Login");
        }
    }
}
