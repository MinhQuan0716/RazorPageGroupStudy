using System.Threading.Tasks;
using Application.IService;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace GroupStudyUI.Pages
{
    public class AskQuestionModel : PageModel
    {
        private readonly IPostService _postService;
        private readonly IHttpContextAccessor _contextAccessor;
        public AskQuestionModel(IPostService postService, IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _postService = postService;
        }


        [BindProperty]
        public int GroupId { get; set; }
        [BindProperty]
        public string PostTitle { get; set; }

        [BindProperty]
        public string Content { get; set; }

        [BindProperty]
        public string PostUrl { get; set; }


        public IActionResult OnGet(int groupId)
        {
            GroupId = groupId;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

			string customerJson = _contextAccessor.HttpContext.Session.GetString("User");
			var user = JsonConvert.DeserializeObject<User>(customerJson);

			await _postService.CreatePost(PostTitle, Content, GroupId, user.Id);

            return RedirectToPage("/Group", new { id = GroupId });
        }

    }
}
