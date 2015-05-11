using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using klukk_social.Models;
using klukk_social.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Security;

namespace klukk_social.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly PostService _postService = new PostService();
        private readonly UserService _userService = new UserService();


		[Authorize(Roles = "Parent")]
		public ActionResult ParentHome()
		{
			var userId = User.Identity.GetUserId();
			var listOfPosts = _postService.GetAllChildrenPosts(userId);
			var user = _userService.FindById(userId);
			UserViewModel profile = new UserViewModel();
			profile.Feed = new List<Post>();
			profile.Feed.AddRange(listOfPosts);
			profile.Person = user;
			return View(profile);
		}


		[Authorize(Roles = "Child")]
		public ActionResult ChildHome()
		{
			var userId = User.Identity.GetUserId();
			var listOfPosts = _postService.GetAllPostsToUser(userId);
			var user = _userService.FindById(userId);
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
        public ActionResult Profile(string userId)
        {
            var listOfPosts = _postService.GetAllPostsToUser(userId);
            var user = _userService.FindById(userId);
            UserViewModel profile = new UserViewModel(_userService.FriendChecker(User.Identity.GetUserId(), userId));
            profile.Request = _userService.GetFriendRequest(User.Identity.GetUserId(), userId);
            profile.Feed = new List<Post>();
            profile.Feed.AddRange(listOfPosts);
            profile.Person = user;
			profile.CurrentUserId = _userService.FindById(User.Identity.GetUserId());
            return View(profile);
        }

        public ActionResult Search(FormCollection searchBar)
        {
            string prefix = searchBar["user-input"];
            List<User> users = _userService.Search(prefix);
            return View(users);
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
			return View();
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
			
			return View();
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
			return View();
		}
		
        [ChildActionOnly]
        public ActionResult ChildSidebarPartial()
        {
            var user = _userService.FindById(User.Identity.GetUserId());
            return PartialView(user);
        }
    }
}
