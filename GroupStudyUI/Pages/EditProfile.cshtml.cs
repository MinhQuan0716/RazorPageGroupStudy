using Application.IService;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GroupStudyUI.Pages
{
    public class EditProfileModel : PageModel
    {
        [BindProperty]
        public User Users { get; set; }
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;
        public EditProfileModel(IUserService userService,IHttpContextAccessor contextAccessor)
        {
            _userService = userService;
            _contextAccessor = contextAccessor;
        }
        public async Task<IActionResult> OnGet(int? id)
        {
            if (!_contextAccessor.HttpContext.Session.Keys.Any())
            {
                return RedirectToPage("/Login");
            }
            bool isAdmin = BitConverter.ToBoolean(_contextAccessor.HttpContext.Session.Get("isAdmin"));
            if (isAdmin)
            {
                return RedirectToPage("/Login");
            }
            
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
