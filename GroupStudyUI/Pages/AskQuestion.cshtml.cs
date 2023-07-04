using System.IO;
using System;
using System.Threading.Tasks;
using Application.IService;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Firebase.Storage;

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

		public async Task<IActionResult> OnPostAsync(IFormFile file)
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			string customerJson = _contextAccessor.HttpContext.Session.GetString("User");
			var user = JsonConvert.DeserializeObject<User>(customerJson);

			string fileName = file.FileName;
			string fileUrl = null;

			if (file.Length > 0)
			{
				// Create a Firebase Storage reference
				var storage = new FirebaseStorage("group-study-prn.appspot.com");

				// Upload the file to Firebase Storage
				var fileRef = storage.Child(fileName);
				await fileRef.PutAsync(file.OpenReadStream());

				// Get the file download URL
				fileUrl = await fileRef.GetDownloadUrlAsync();

			}

			await _postService.CreatePost(PostTitle, Content, GroupId, user.Id, fileUrl);

			return RedirectToPage("/Group", new { id = GroupId });
		}


	}
}
