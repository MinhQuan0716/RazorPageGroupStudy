using Application.IService;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GroupStudyUI.Pages
{
    public class EditProfileModel : PageModel
    {
        [BindProperty]
        public User infoUser { get; set; }
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;
        public EditProfileModel(IUserService userService,IHttpContextAccessor contextAccessor)
        {
            _userService = userService;
            _contextAccessor = contextAccessor;
        }
        public async Task<IActionResult> OnGet(int id)
        {
            string isLogin = HttpContext.Session.GetString("isLogin");

            if (isLogin == null || isLogin.Equals("false"))
            {
                return RedirectToPage("/Login");
            }

            infoUser = await _userService.GetUserById(id);
            if (infoUser == null)
            {
                return NotFound();
            }

            return Page();
        }
        public async Task<IActionResult> OnPost(int userId)
        {
            var updateUser = await _userService.GetUserById(userId);

            if (updateUser != null)
            {
                // Update the user information with the new values
                updateUser.Name = infoUser.Name;
                updateUser.Email = infoUser.Email;
                updateUser.Password = infoUser.Password;
                updateUser.RoleId = 2;
                // Save the changes to the user
                await _userService.UpdateUser(updateUser);
            }

            string newUserInfo = JsonConvert.SerializeObject(updateUser);
            _contextAccessor.HttpContext.Session.SetString("User", newUserInfo);
            return RedirectToPage("/Information");

        }
    }
}
