using Application.IService;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace GroupStudyUI.Pages
{
    public class InformationModel : PageModel
    {
        [BindProperty]
        public User User { get; set; }
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;
        public InformationModel (IUserService userService,IHttpContextAccessor contextAccessor)
        {
            _userService = userService;
            _contextAccessor = contextAccessor;
        }
        public void OnGet()
        {
            if (_contextAccessor.HttpContext.Session.Keys.Any())
            {
                var userLoginData = _contextAccessor.HttpContext.Session.GetString("User");
                var userLogin = JsonConvert.DeserializeObject<User>(userLoginData);
                User= userLogin;
                User.Id = userLogin.Id;
            }
        }
  
    }
}
