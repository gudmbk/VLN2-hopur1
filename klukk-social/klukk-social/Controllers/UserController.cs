using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using klukk_social.Models;
using klukk_social.Services;
using Microsoft.AspNet.Identity;

namespace klukk_social.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private PostService ps = new PostService();
        private UserSerice us = new UserSerice();

		[Authorize(Roles = "Parent")]
		public ActionResult ParentHome()
		{

			return View();
		}


		[Authorize(Roles = "Child")]
        public ActionResult ChildHome()
		{
		    var userId = User.Identity.GetUserId();
		    var listOfPosts = ps.GetAllPostsToUser(userId);
		    var user = us.FindById(userId);
            UserViewModel profile = new UserViewModel();
            profile.Feed = new List<Post>();
            profile.Feed.AddRange(listOfPosts);
		    profile.Person = user;
            return View(profile);
        }

		[Authorize(Roles = "Parent")]
        public ActionResult CreateChild()
        {
			return RedirectToAction("CreateChild", "Account");
        }

        public ActionResult FriendHome(string userId)
        {
            var listOfPosts = ps.GetAllPostsToUser(userId);
            var user = us.FindById(userId);
            UserViewModel profile = new UserViewModel();
            profile.Feed = new List<Post>();
            profile.Feed.AddRange(listOfPosts);
            profile.Person = user;
            return View("ChildHome", profile);
        }

        public ActionResult Search(FormCollection searchBar)
        {
            string prefix = searchBar["user-input"];
            List<User> users = us.Search(prefix);
            return View(users);
        }

        public ActionResult SendFriendRequest()
        {
            string myId = User.Identity.GetUserId();
            return View();
        }
    }
}
