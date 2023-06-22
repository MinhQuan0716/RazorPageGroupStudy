using Application.IService;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupStudyUI.Pages
{
    public class GroupModel : PageModel
    {
		private readonly IGroupService _groupService;
		private readonly IHttpContextAccessor _contextAccessor;
		public GroupModel(IGroupService groupService, IHttpContextAccessor contextAccessor)
		{
			_groupService = groupService;
			_contextAccessor = contextAccessor;
		}
		public List<Group> Groups { get; set; }
		public async Task<IActionResult> OnGet()
        {
			if (_contextAccessor.HttpContext.Session.Keys.Any())
			{
				string customerJson = _contextAccessor.HttpContext.Session.GetString("User");
				if(customerJson != null)
				{
					var user=JsonConvert.DeserializeObject<User>(customerJson);
				Groups=await _groupService.GetJoinedGroup(user.Id);
					return Page();
				}
			}
			return RedirectToPage("/Login");
        }
    }
}
