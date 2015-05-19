using System.Collections.Generic;
using System.Web.Mvc;
using klukk_social.Models;
using klukk_social.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace klukk_social.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly PostService _postService = new PostService(null);
        private readonly UserService _userService = new UserService(null);
		private readonly GroupService _groupService = new GroupService(null);


		[Authorize(Roles = "Parent")]
		public ActionResult ParentHome()
		{
			var userId = User.Identity.GetUserId();
			var listOfPosts = _postService.GetAllChildrenPosts(userId);
			var user = _userService.FindById(userId);
			var listOfParentsChildren = _userService.GetAllChildren(userId);
			UserViewModel profile = new UserViewModel();
			profile.Feed = new List<Post>();
			profile.Feed.AddRange(listOfPosts);
			profile.Person = user;
			profile.AllChildren = listOfParentsChildren;
			return View(profile);
		}


		[Authorize(Roles = "Child")]
		public ActionResult ChildHome()
		{
			var userId = User.Identity.GetUserId();
			var listOfPosts = _postService.GetAllPostForUserFeed(userId);
			var user = _userService.FindById(userId);
			UserViewModel profile = new UserViewModel();
			profile.Feed = new List<Post>();
			profile.Feed.AddRange(listOfPosts);
			profile.Person = user;
			profile.CurrentUser = _userService.FindById(User.Identity.GetUserId());
			return View(profile);
		}

		[Authorize(Roles = "Parent")]
        public ActionResult CreateChild()
        {
			return RedirectToAction("CreateChild", "Account");
        }

        [Authorize(Roles = "Child")]
        public ActionResult Profile(string userId)
        {
            var listOfPosts = _postService.GetAllPostsToUser(userId);
            var user = _userService.FindById(userId);
			UserViewModel profile = new UserViewModel(_userService.FriendChecker(User.Identity.GetUserId(), userId));
            profile.Request = _userService.GetFriendRequest(User.Identity.GetUserId(), userId);
            profile.Feed = new List<Post>();
            profile.Feed.AddRange(listOfPosts);
            profile.Person = user;
			profile.CurrentUser = _userService.FindById(User.Identity.GetUserId());
            bool isFriend = _userService.FriendChecker(User.Identity.GetUserId(), user.Id);
            ViewBag.isFriend = isFriend;
            return View(profile);
        }

        public ActionResult SearchButton(FormCollection searchBar)
        {
            string prefix = searchBar["user-input"];
			return RedirectToAction("Search", "User", new { prefix = prefix } );
        }

		public ActionResult Search(string prefix)
		{
			if (prefix == null)
			{
				prefix = "";
			}
			var me = User.Identity.GetUserId();
			List<UserWithFriendship> users = _userService.SearchUsersWithFriendship(prefix, me);
			List<GroupWithMembership> groups = _groupService.SearchGroupsWithMemership(prefix, me);
			var model = new SearchViewModel(groups, users, prefix);


			return View(model);
		}

		public ActionResult SendFriendRequest(string newFriendId, string searchString)
		{
			if (newFriendId == null)
			{
				return View();
			}
			FriendRequest friendRequest = new FriendRequest();
			friendRequest.FromUserId = User.Identity.GetUserId();
			friendRequest.ToUserId = newFriendId;
			_userService.SendFriendRequest(friendRequest);
			return RedirectToAction("Search", "User", new { prefix = searchString });
		}

		public ActionResult CancelFriendRequestFromSearch(string newFriendId, string searchString)
		{
			FriendRequest friendRequest = new FriendRequest();
			friendRequest.FromUserId = User.Identity.GetUserId();
			friendRequest.ToUserId = newFriendId;
			_userService.DeleteFriendRequest(friendRequest);
			return RedirectToAction("Search", "User", new { prefix = searchString });
		}

		public ActionResult acceptFriendRequestFromSearch(string newFriendId, string searchString)
		{
			FriendRequest accept = _userService.GetFriendRequest(newFriendId, User.Identity.GetUserId());
			Friendship friends = new Friendship
			{
				FromUserId = accept.FromUserId,
				ToUserId = User.Identity.GetUserId(),
			};
			accept.ToUserId = User.Identity.GetUserId();
			_userService.MakeFriends(friends);
			_userService.DeleteFriendRequest(accept);

			return RedirectToAction("Search", "User", new { prefix = searchString });
		}

        [HttpPost]
        public ActionResult SendFriendRequest(FriendRequest json)
        {
            FriendRequest friendRequest = new FriendRequest();
            friendRequest.FromUserId = User.Identity.GetUserId();
            friendRequest.ToUserId = json.ToUserId;
            _userService.SendFriendRequest(friendRequest);
            return null;
        }

        [HttpPost]
        public ActionResult AcceptFriendRequest(FriendRequest accept)
        {
            Friendship friends = new Friendship
            {
                FromUserId = accept.FromUserId,
                ToUserId = User.Identity.GetUserId(),
            };
            accept.ToUserId = User.Identity.GetUserId();
            _userService.MakeFriends(friends);
            _userService.DeleteFriendRequest(accept);

            return null;
        }

        public ActionResult CancelFriendRequest(FriendRequest cancel)
        {
            cancel.FromUserId = User.Identity.GetUserId();
            _userService.DeleteFriendRequest(cancel);

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
			var newProfilePicUrl = form["picURL"];
			var manager = new UserManager<User>(new UserStore<User>(new ApplicationDbContext()));
			var currentUser = manager.FindById(User.Identity.GetUserId());
			currentUser.ProfilePic = newProfilePicUrl;
			manager.Update(currentUser);
			return View();
		}
		[Authorize(Roles = "Parent")]
		public ActionResult ParentSettings()
		{
			UserViewModel viewBag = new UserViewModel();
			viewBag.AllChildren = _userService.GetAllChildren(User.Identity.GetUserId());
			return View(viewBag);
		}

		[HttpPost]
		[Authorize(Roles = "Parent")]
		public ActionResult ParentSettings(FormCollection form)
		{

			var newProfilePicUrl = form["picURL"];
			if (newProfilePicUrl != "")
			{
			    var manager = new UserManager<User>(new UserStore<User>(new ApplicationDbContext())); 
				var currentUser = manager.FindById(User.Identity.GetUserId());
				currentUser.ProfilePic = newProfilePicUrl;
				manager.Update(currentUser);
			}

			return ParentSettings();
		}

		public ActionResult FriendsList()
		{
		    var userId = User.Identity.GetUserId();
		    var friendRequests = _userService.GetFriendRequest(userId);
		    var friends = _userService.GetFriends(userId);
            FriendsViewModel list = new FriendsViewModel();
            list.Friends.AddRange(friends);
            list.FriendRequests.AddRange(friendRequests);
            
		    return View(list);
		}

		public ActionResult DeleteCurrentUser() // WARNING
		{
			//skrifa?
			return null;
		}
		
        [ChildActionOnly]
        public ActionResult ChildSidebarPartial()
        {
            var user = _userService.FindById(User.Identity.GetUserId());
            return PartialView("ChildSidebarPartial", user);
        }
        [ChildActionOnly]
        public ActionResult ParentSidebarPartial()
        {
            var user = _userService.FindById(User.Identity.GetUserId());
            ViewBag.Children = _userService.GetAllChildren(user.Id);
            return PartialView("ParentSidebarPartial", user);
        }

        public ActionResult GoToChild()
        {
            return null;
        }

        [Authorize(Roles = "Parent")]
        public ActionResult Reports()
        {
            User parent = _userService.FindById(User.Identity.GetUserId());
			List<GroupRequest> groupRequests = _groupService.GetAllRequests(User.Identity.GetUserId());
			ReportModelView reports = new ReportModelView();
			reports.Parent = parent;
			reports.GroupRequests = groupRequests;
			return View(reports);
        }

        public ActionResult DeleteFriend(string friendId)
        {
            _userService.RemoveFriendship(User.Identity.GetUserId(), friendId);
            return null;
        }
    }
}
