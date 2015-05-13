using klukk_social.Models;
using klukk_social.Services;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;

namespace klukk_social.Controllers
{
    public class GroupController : Controller
    {
		GroupService _groupService = new GroupService();
		UserService _userService = new UserService();
		PostService _postService = new PostService();

		[Authorize(Roles = "Parent")]
        public ActionResult CreateGroup()
        {
            return View();
        }

        [HttpPost]
		[Authorize(Roles = "Parent")]
        public ActionResult CreateGroup(FormCollection collection)
        {
			if (String.IsNullOrEmpty(collection["Name"]))
			{
				return RedirectToAction("CreateGroup", "Group");
			}
			Group newGroup = new Group();
			newGroup.Date = DateTime.Now;
			newGroup.Description = collection["Description"];
			newGroup.Name = collection["Name"];
			newGroup.UserId = User.Identity.GetUserId();

			_groupService.CreateGroup(newGroup);
            return View();
        }
		
		public ActionResult Index()
		{
			GroupService _groupService = new GroupService();
			GroupViewModel bag = new GroupViewModel();
			bag.GroupList = _groupService.GetAllGroups(User.Identity.GetUserId());
			return View(bag);
		}
		public ActionResult OwnedGroups()
		{
			return View();
		}
		[Authorize(Roles = "Child")]
		public ActionResult Profile(int? groupId)
		{
			if (groupId.HasValue)
			{
				var listOfPosts = _groupService.GetAllGroupPostsToGroup(groupId);
				var group = _groupService.FindById(groupId);
				GroupViewModel groupWall = new GroupViewModel();
	
				groupWall.Feed = new List<Post>();
				groupWall.Feed.AddRange(listOfPosts);
				groupWall.Group = group;
				return View(groupWall);
			}

			return RedirectToAction("ChildHome", "User");
			
		}

		[HttpPost]
		public ActionResult PostOnGroup(FormCollection collection)
		{
			Post post = new Post();
			string text = collection["status"];
			if (String.IsNullOrEmpty(text))
			{
				return RedirectToAction("Index", "Groups");
			}
			post.Text = text;
			post.FromUserId = User.Identity.GetUserId();
			post.GroupId = Convert.ToInt32(collection["GroupId"]);
			post.PosterName = _userService.GetFullNameById(User.Identity.GetUserId());
			post.ToUserId = User.Identity.GetUserId();
			if (post.FromUserId != null)
			{
				_postService.AddPost(post);
				return RedirectToAction("ChildHome", "User");
			}

			return View("Error");
		}
    }
}