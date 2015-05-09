﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using klukk_social.Models;
using klukk_social.Services;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using Microsoft.AspNet.Identity.EntityFramework;

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
        [Authorize(Roles = "Child")]
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
        [HttpPost]
        public ActionResult SendFriendRequest(string userToAdd)
        {
            FriendRequest friendRequest = new FriendRequest();
            friendRequest.FromUserId = User.Identity.GetUserId();
            friendRequest.ToUserId = userToAdd;
            us.SendFriendRequest(friendRequest);
            return null;
        }

		[Authorize(Roles = "Child")]
		public ActionResult ChildSettings()
		{
			return View();
		}

		[HttpPost]
		[Authorize(Roles = "Child")]
		public ActionResult ChildSettings(FormCollection form)
		{

			var NewProfilePicURL = form["picURL"];
			var manager = new UserManager<User>(new UserStore<User>(new ApplicationDbContext()));
			var currentUser = manager.FindById(User.Identity.GetUserId());
			currentUser.ProfilePic = NewProfilePicURL;
			manager.Update(currentUser);
			return View();
		}

		public ActionResult AddEmptyProfilePic() //óþarfi
		{
			var manager = new UserManager<User>(new UserStore<User>(new ApplicationDbContext()));
			var currentUser = manager.FindById(User.Identity.GetUserId());

			currentUser.ProfilePic = "/Content/Images/EmptyProfilePicture.gif";
			manager.Update(currentUser);
			return RedirectToAction("Index");
		}
    }
}
