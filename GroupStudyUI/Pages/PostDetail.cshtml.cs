using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.IService;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace GroupStudyUI.Pages
{
    public class PostDetailModel : PageModel
    {
        private readonly IGroupService _groupService;
        private readonly IUserService _userService;
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly IHttpContextAccessor _contextAccessor;
        public PostDetailModel(IGroupService groupService, IPostService postService, IHttpContextAccessor contextAccessor, IUserService userService, ICommentService commentService)
        {
            _groupService = groupService;
            _contextAccessor = contextAccessor;
            _postService = postService;
            _userService = userService;
            _commentService = commentService;
        }
        [BindProperty]
        public Post Post { get; set; }
        public User AuthorPost { get; set; }
        public List<Comment> listAllCommentOfPost { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            Post = await _postService.GetPostById(id);
            AuthorPost = await _userService.GetUserById(Post.CreateUserId);
            listAllCommentOfPost = await _commentService.GetAllCommentByPostId(id);

            if (Post == null)
            {
                return NotFound();
            }

            string customerJson = _contextAccessor.HttpContext.Session.GetString("User");
            var user = JsonConvert.DeserializeObject<User>(customerJson);

            return Page();
        }

    }
}
