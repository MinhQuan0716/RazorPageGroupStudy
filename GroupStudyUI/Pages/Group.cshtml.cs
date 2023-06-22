using Application.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
		public void OnGet()
        {
			
        }
    }
}
