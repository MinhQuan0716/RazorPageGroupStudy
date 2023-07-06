using System;
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
        private readonly ICommentMapService _commentMapService;
        public PostDetailModel(IGroupService groupService, IPostService postService, IHttpContextAccessor contextAccessor, IUserService userService, ICommentService commentService,ICommentMapService commentMapService)
        {
            _groupService = groupService;
            _contextAccessor = contextAccessor;
            _postService = postService;
            _userService = userService;
            _commentService = commentService;
            _commentMapService = commentMapService;
        }
        [BindProperty]
        public Post Post { get; set; }
        [BindProperty]
        public User AuthorPost { get; set; }
        [BindProperty]
        public List<Comment> listAllCommentOfPost { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            string isLogin = HttpContext.Session.GetString("isLogin");

            if (isLogin == null || isLogin.Equals("false"))
            {
                return RedirectToPage("/Login");
            }

            Post = await _postService.GetPostById(id);
            AuthorPost = await _userService.GetUserById(Post.CreateUserId);
            listAllCommentOfPost = await _commentService.GetAllCommentByPostId(id);

            if (Post == null)
            {
                return NotFound();
            }

            return Page();
        }

		public async Task<IActionResult> OnPostAddComment(int postId, string content)
		{
			string customerJson = _contextAccessor.HttpContext.Session.GetString("User");
			var user = JsonConvert.DeserializeObject<User>(customerJson);

            await _commentService.CreateComment(user.Id, postId, content);

			return RedirectToPage("/PostDetail", new { id = postId });
		}

		public async Task<IActionResult> OnPostAddReply(int postId, string content, int replyCommentId)
		{
			string customerJson = _contextAccessor.HttpContext.Session.GetString("User");
			var user = JsonConvert.DeserializeObject<User>(customerJson);

			await _commentService.ReplyComment(user.Id, postId, content, replyCommentId);

			return RedirectToPage("/PostDetail", new { id = postId });
		}
        public async Task<IActionResult> OnPostDeleteComment(int? commentId)
        {
            if (commentId == null)
            {
                return NotFound();
            }
			string customerJson = _contextAccessor.HttpContext.Session.GetString("User");
			var user = JsonConvert.DeserializeObject<User>(customerJson);
			Comment findComment = await _commentService.GetCommentById(commentId.Value);
			if (findComment.CreateByUserId != user.Id)
			{
				return BadRequest("You cannot remove this comment");
			}
			List<CommentMap> list = await _commentMapService.GetAllReplyComment(commentId.Value);
            if(list.Count == 0)
            {
				bool isDelete = await _commentService.DeleteComment(commentId.Value);
				if (!isDelete)
				{
					return BadRequest();
				}
                return Page();
			}
		
			
			bool isChildDelete = await _commentMapService.DeleteAllReplyComment(commentId.Value);
			if (!isChildDelete)
            {
                return BadRequest();
            } else
            {
                bool isParentDelete = await _commentService.DeleteComment(commentId.Value);
				if (!isParentDelete)
                {
                    return BadRequest();
                }
            }
            return Page();
        }

	}
}
