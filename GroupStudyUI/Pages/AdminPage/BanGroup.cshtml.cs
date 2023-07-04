using Application.IService;
using Application.Service;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupStudyUI.Pages.AdminPage
{
    public class BanGroupModel : PageModel
    {
        [BindProperty]
        public Group Groups { get; set; }
        private readonly IGroupService _groupService;
        public BanGroupModel( IGroupService groupService)
        {
            _groupService = groupService;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Groups = await _groupService.GetGroupBydId(id.Value);
            if (Groups == null)
            {
                return NotFound();
            }
            return Page();

        }
        public async Task<IActionResult> OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Groups = await _groupService.GetGroupBydId(id.Value);
            if (Groups != null)
            {
                await _groupService.SoftRemoveGroup(id.Value);
            }
            return RedirectToPage("/AdminPage/GroupIndex");

        }
    }
}

