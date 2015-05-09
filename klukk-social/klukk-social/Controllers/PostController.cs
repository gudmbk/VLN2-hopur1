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
        PostService _postService = new PostService();
        UserSerice _userService = new UserSerice();

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
            post.FromUserId = User.Identity.GetUserId();
            post.ToUserId = collection["toUserId"];
            post.PosterName = _userService.GetFullNameById(User.Identity.GetUserId());
            if (post.FromUserId != null)
            {
                _postService.AddPost(post);
                return RedirectToAction("ChildHome", "User");
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
                return RedirectToAction("ChildHome", "User");
            }
            
            return View("Error");
        }
    }
}