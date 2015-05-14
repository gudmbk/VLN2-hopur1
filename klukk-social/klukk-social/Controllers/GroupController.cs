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
        readonly GroupService _groupService = new GroupService();
        readonly UserService _userService = new UserService();
        readonly PostService _postService = new PostService();

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
			newGroup.Description = collection["description"];
			newGroup.Name = collection["name"];
			newGroup.UserId = User.Identity.GetUserId();
			if (!String.IsNullOrEmpty(collection["opengroup"]))
			{
				newGroup.OpenGroup = true;
			}
			if (!String.IsNullOrEmpty(collection["profilepicurl"]))
			{
				newGroup.ProfilePic = collection["profilepicurl"];
			}
			_groupService.CreateGroup(newGroup);
            return View();
        }
		
		public ActionResult Index()
		{
			GroupViewModel bag = new GroupViewModel();
			bag.GroupList = _groupService.GetAllGroups(User.Identity.GetUserId());
			return View(bag);
		}

		public ActionResult ParentGroups()
		{
			GroupViewModel bag = new GroupViewModel();
			bag.GroupList = _groupService.GetAllParentGroups(User.Identity.GetUserId());
			return View(bag);
		}

		[Authorize(Roles = "Child")]
		public ActionResult Profile(int? groupId)
		{
			if (groupId.HasValue)
			{
				var listOfPosts = _groupService.GetAllGroupPostsToGroup(groupId);
				var group = _groupService.FindById(groupId);
				GroupViewModel groupWall = new GroupViewModel();
				groupWall.Request = _groupService.GetGroupRequest(User.Identity.GetUserId(), groupId);
				groupWall.Feed = new List<Post>();
				groupWall.Feed.AddRange(listOfPosts);
				groupWall.Group = group;
				groupWall.MemberOfGroup = _groupService.IsUserMember(groupId.Value, User.Identity.GetUserId());
				groupWall.GroupList = _groupService.GetAllGroups(User.Identity.GetUserId());
				groupWall.CurrentUser = _userService.FindById(User.Identity.GetUserId());
				return View(groupWall);
			}

			return RedirectToAction("Index", "Group");
			
		}

		[HttpPost]
		public ActionResult PostOnGroup(FormCollection collection)
		{
			Post post = new Post();
			string text = collection["status"];
			if (String.IsNullOrEmpty(text))
			{
				return RedirectToAction("Profile", "Group", new { groupId = post.GroupId });
			}
			post.Text = text;
			post.HtmlText = Helpers.ParseText(text);
			post.FromUserId = User.Identity.GetUserId();
			post.GroupId = Convert.ToInt32(collection["GroupId"]);
			post.PosterName = _userService.GetFullNameById(User.Identity.GetUserId());
			post.ToUserId = User.Identity.GetUserId(); // Get ekki tekið út
			if (post.FromUserId != null)
			{
				_postService.AddPost(post);
				return RedirectToAction("Profile", "Group", new { groupId = post.GroupId  });
			}

			return View("Error");
		}

		public ActionResult SendGroupRequest(int? groupId)
		{
			if (groupId.HasValue)
			{
				GroupRequest groupRequest = new GroupRequest();
				groupRequest.FromUserId = User.Identity.GetUserId();
				groupRequest.GroupId = groupId.Value;
				_groupService.SendGroupRequest(groupRequest);
				return RedirectToAction("Profile", "Group", new { groupId = groupId.Value });
			}
			return RedirectToAction("index", "Group");
		}

		public ActionResult DeleteGroupRequest(int? groupId)
		{
			if (groupId.HasValue)
			{
				_groupService.DeleteGroupRequest(groupId.Value, User.Identity.GetUserId());
				return RedirectToAction("Profile", "Group", new { groupId = groupId.Value });
			}
			return RedirectToAction("index", "Group");
		}

		public ActionResult JoinOpenGroup(int? groupId)
		{
			if (groupId.HasValue)
			{
				GroupUsers newUser = new GroupUsers();
				newUser.GroupId = groupId.Value;
				newUser.UserId = User.Identity.GetUserId();
				_groupService.AcceptGroupRequest(newUser);
				return RedirectToAction("Profile", "Group", new { groupId = groupId });
			}

			return RedirectToAction("index", "Group");
		}

		public ActionResult LeaveOpenGroup(int? groupId)
		{
			if (groupId.HasValue)
			{
				_groupService.LeaveGroup(User.Identity.GetUserId(), groupId.Value);
				return RedirectToAction("Profile", "Group", new { groupId = groupId });
			}

			return RedirectToAction("index", "Group");
		}

		public ActionResult GroupSettings(int? groupId)
		{
			if (groupId.HasValue)
			{
				GroupViewModel group = new GroupViewModel();
				group.Group = _groupService.FindById(groupId.Value);
				return View(group);
			}

			return RedirectToAction("ParentGroups", "Group");
		}

		[HttpPost]
		[Authorize(Roles = "Parent")]
		public ActionResult GroupSettings(FormCollection collection)
		{
			if (String.IsNullOrEmpty(collection["Name"]) || String.IsNullOrEmpty(collection["groupid"]))
			{
				return RedirectToAction("CreateGroup", "Group");
			}
			Group newGroup = _groupService.FindById(Convert.ToInt32(collection["GroupId"]));
			newGroup.Description = collection["description"];
			newGroup.Name = collection["name"];
			if (!String.IsNullOrEmpty(collection["opengroup"]))
			{
				newGroup.OpenGroup = true;
			}
			if (!String.IsNullOrEmpty(collection["profilepicurl"]))
			{
				newGroup.ProfilePic = collection["profilepicurl"];
			}
			_groupService.UpdateGroup(newGroup);

			return RedirectToAction("ParentGroups", "Group");
		}
    }
}