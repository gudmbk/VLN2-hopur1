using klukk_social.Models;
using klukk_social.Services;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace klukk_social.Controllers
{
    public class GroupController : Controller
    {
        readonly GroupService _groupService = new GroupService(null);
        readonly UserService _userService = new UserService(null);
        readonly PostService _postService = new PostService(null);
        HelperController _helper = new HelperController();


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
			var me = User.Identity.GetUserId();
			List<GroupWithMembership> groups = _groupService.SearchGroupsOfUser(me);
			var model = new SmallCardModel(groups, null, me);

			return View(model);
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
			post.ToUserId = null; // Get ekki tekið út
			if (post.FromUserId != null)
			{
				_postService.AddPost(post);
				return RedirectToAction("Profile", "Group", new { groupId = post.GroupId  });
			}

			return View("Error");
		}

		public ActionResult SendGroupRequest(int? groupId, string ReturnUrl, string Title)
		{
			if (groupId.HasValue)
			{
				GroupRequest groupRequest = new GroupRequest();
				groupRequest.FromUserId = User.Identity.GetUserId();
				groupRequest.GroupId = groupId.Value;
				_groupService.SendGroupRequest(groupRequest);
				
			}
			if(Title == "Leit")
			{
				return RedirectToAction("Search", "User", new { prefix = ReturnUrl });
			}
			else if(Title == "Hópar"){
				return RedirectToAction("FriendsList", "User");
			}
			return RedirectToAction("Profile", "Group", new { groupId = groupId.Value });
		}

		public ActionResult DeleteGroupRequest(int? groupId, string ReturnUrl, string Title)
		{
			if (groupId.HasValue)
			{
				_groupService.DeleteGroupRequest(groupId.Value, User.Identity.GetUserId());
			}
			if (Title == "Leit")
			{
				return RedirectToAction("Search", "User", new { prefix = ReturnUrl });
			}
			else if (Title == "Hópar")
			{
				return RedirectToAction("Index", "Group");
			}
			return RedirectToAction("Profile", "Group", new { groupId = groupId.Value });
		}

		public ActionResult JoinOpenGroup(int? groupId, string ReturnUrl, string Title)
		{
			if (groupId.HasValue)
			{
				GroupUsers newUser = new GroupUsers();
				newUser.GroupId = groupId.Value;
				newUser.UserId = User.Identity.GetUserId();
				_groupService.AcceptGroupRequest(newUser);
			}

			if (Title == "Leit")
			{
				return RedirectToAction("Search", "User", new { prefix = ReturnUrl });
			}
			else if (Title == "Hópar")
			{
				return RedirectToAction("FriendsList", "User");
			}
			return RedirectToAction("Profile", "Group", new { groupId = groupId.Value });
		}

		public ActionResult LeaveOpenGroup(int? groupId)
		{
			if (groupId.HasValue)
			{
				_groupService.LeaveGroup(User.Identity.GetUserId(), groupId.Value);
				return RedirectToAction("Profile", "Group", new { groupId });
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
		public ActionResult GroupSettings(GroupViewModel model)
		{
			if (ModelState.IsValid)
			{
				Group newGroup = _groupService.FindById(model.Group.Id);
				newGroup.Description = model.Group.Description;
				newGroup.Name = model.Group.Name;
				newGroup.OpenGroup = model.Group.OpenGroup;
				if (model.Group.ProfilePic != null)
				{
					newGroup.ProfilePic = model.Group.ProfilePic;
				}
				_groupService.UpdateGroup(newGroup);
				
				return RedirectToAction("ParentGroups", "Group");
			}
			return View(model);
		}

		public ActionResult GrantAccessToGroup(int? requestId)
		{
			if (requestId.HasValue)
			{
				GroupUsers newUser = new GroupUsers();
				newUser.UserId = _groupService.GetRequestUserId(requestId.Value);
				newUser.GroupId = _groupService.GetGroupRequestGroupId(requestId);
				_groupService.AcceptGroupRequest(newUser);
				_groupService.DeleteGroupRequest(requestId.Value);
				return RedirectToAction("Reports", "User");
			}

			return RedirectToAction("Reports", "User");
		}

		public ActionResult RefuseAccessToGroup(int? requestId)
		{
			if (requestId.HasValue)
			{
				_groupService.DeleteGroupRequest(requestId.Value);
				return RedirectToAction("Reports", "User");
			}

			return RedirectToAction("Reports", "User");
		}

        [HttpPost]
        public ActionResult PostPicture(FormCollection collection)
        {
            var file = Request.Files["file"];
            if (file != null && file.ContentLength > 0)
            {
                Post post = new Post();
                post.ToUserId = null;
                post.FromUserId = User.Identity.GetUserId();
                if (post.FromUserId != null)
                {
                    post.GroupId = Convert.ToInt32(collection["GroupId"]);
                    post.PosterName = _userService.GetFullNameById(User.Identity.GetUserId());
                    _postService.AddPost(post);
                    string url = _helper.UploadPicture(file, post.Id.ToString(), "post", new Size(1024, 768));
                    _postService.AddPicUrl(post.Id, url);
                    return RedirectToAction("Profile", "Group", new { groupId = post.GroupId });
                }
            }
            return View("Error");
        }

        [HttpPost]
        public ActionResult PostVideo(FormCollection collection)
        {
            string link = collection["link"];

            string frame = Helpers.ParseText(link);
            Post post = new Post();
            post.ToUserId = null;
            post.FromUserId = User.Identity.GetUserId();
            post.GroupId = Convert.ToInt32(collection["GroupId"]);
            post.HtmlText = frame;
            post.VideoUrl = frame;
            post.Text = link;
            if (post.GroupId != null)
            {
                _postService.AddPost(post);
                return RedirectToAction("Profile", "Group", new { groupId = post.GroupId });
            }
            return View("Error");
        }
    }
}