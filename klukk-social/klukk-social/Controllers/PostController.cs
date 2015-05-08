using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using klukk_social.Models;
using klukk_social.Services;
using Microsoft.AspNet.Identity;

namespace klukk_social.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        PostService postService = new PostService();
        UserSerice userService = new UserSerice();
        public ActionResult Index()
        {
            return View();
        }

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
            post.Date = DateTime.Now;
            post.FromUserId = User.Identity.GetUserId();
            post.ToUserId = User.Identity.GetUserId();
            if (post.FromUserId != null)
            {
                postService.AddPost(post);
                return RedirectToAction("ChildHome", "User");
            }
            
            return View("Error");
        }
    }
}