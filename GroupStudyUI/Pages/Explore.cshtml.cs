using System.Collections.Generic;
using System.Threading.Tasks;
using Application.IService;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GroupStudyUI.Pages
{
    public class ExploreModel : PageModel
    {
		private readonly IGroupService _groupService;
		private readonly IHttpContextAccessor _contextAccessor;
		public ExploreModel(IGroupService groupService, IHttpContextAccessor contextAccessor)
		{
			_groupService = groupService;
			_contextAccessor = contextAccessor;
		}
        public List<Group> listGroups { get; set; }
        [BindProperty]
        public string searchName { get; set; }
        public async Task OnGet()
        {
            IEnumerable<Group> groups = await _groupService.GetAllGroupV2();
            listGroups = new List<Group>(groups);
        }
        public async Task<IActionResult> OnPostSearch() 
        {
            if (searchName == null)
            {
                IEnumerable<Group> group = await _groupService.GetAllGroupV2();
                listGroups = new List<Group>(group);
               return Page();  
            }
            List<Group> groups = new List<Group>();
            groups=await _groupService.SearchGroupByName(searchName);
            listGroups=groups;
            return Page();
        }
    }
}
