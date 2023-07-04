using Application.IService;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupStudyUI.Pages.AdminPage
{
    public class GroupIndexModel : PageModel
    {
        [BindProperty]
        public List<Group> Groups { get; set; }
        private readonly IGroupService _groupService;
        public GroupIndexModel(IGroupService groupService)
        {
            _groupService = groupService;
        }
        public async Task OnGet()
        {
            Groups = await  _groupService.GetAllGroupV3();
        }
        public IActionResult OnPostLogout()
        {
            return RedirectToPage("/Login");
        }
    }
}
