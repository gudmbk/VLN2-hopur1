using System;
using System.Web.Mvc;
using klukk_social.Models;
using klukk_social.Services;
using Microsoft.AspNet.Identity;

namespace klukk_social.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        readonly PostService _postService = new PostService();
        readonly UserService _userService = new UserService();

        [HttpPost]
        public ActionResult PostStatus(FormCollection collection)
        {
            Post post = new Post();
            string text = collection["status"];
            if (String.IsNullOrEmpty(text))
            {
                return RedirectToAction("ChildHome", "User");
            }
            post.Text = text;
            post.HtmlText = Helpers.ParseText(text);
            post.FromUserId = User.Identity.GetUserId();
            post.ToUserId = collection["toUserId"];
            post.PosterName = _userService.GetFullNameById(User.Identity.GetUserId());
            if (post.FromUserId != null)
            {
                _postService.AddPost(post);
				return RedirectToAction("Profile", "User", new { userId = post.ToUserId });
            }
            
            return View("Error");
        }

        public ActionResult PostComment(FormCollection collection)
        {
            Comment comment = new Comment();
            string text = collection["comment"];
            string id = collection["PostId"];

            int postId = Int32.Parse(id);
            if (String.IsNullOrEmpty(text))
            {
                return RedirectToAction("ChildHome", "User");
            }
            comment.Body = text;
            comment.UserId = User.Identity.GetUserId();
            comment.PostId = postId;
            comment.PosterName = _userService.GetFullNameById(User.Identity.GetUserId());
            if (comment.UserId != null)
            {
                _postService.AddComment(comment);
				return RedirectToAction("Profile", "User", new { userId = _postService.GetToUserIdPostId(postId)});
            }
            return View("Error");
        }

        public ActionResult RemovePost(int itemId)
        {
            _postService.RemovePost(itemId);
            return null;
        }
        public ActionResult RemoveComment(int itemId)
        {
            _postService.RemoveComment(itemId);
            return null;
        }
        public ActionResult ReportPost(int itemId)
        {
            string reporterId = User.Identity.GetUserId();
            _postService.AddReport(itemId, reporterId, true);
            return null;
        }
        public ActionResult ReportComment(int itemId)
        {
            string reporterId = User.Identity.GetUserId();
            _postService.AddReport(itemId, reporterId, false);
            return null;
        }
    }
}