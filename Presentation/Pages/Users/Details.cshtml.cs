using Application.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.Users
{
    public class UserListModel : PageModel
    {
        private readonly IUserRepo _userRepo;

        public UserListModel(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task OnGet()
        {
            var userList = await _userRepo.GetAllAsync();
            ViewData["User"] = userList.First();

        }
    }
}
