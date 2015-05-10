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
        private PostService _postService = new PostService();
        private UserService _userSerice = new UserService();

		[Authorize(Roles = "Parent")]
		public ActionResult ParentHome()
		{
			var userId = User.Identity.GetUserId();
			var listOfPosts = _postService.GetAllChildrenPosts(userId);
			var user = _userSerice.FindById(userId);
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
			var user = _userSerice.FindById(userId);
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
            var user = _userSerice.FindById(userId);
            UserViewModel profile = new UserViewModel();
            profile.Feed = new List<Post>();
            profile.Feed.AddRange(listOfPosts);
            profile.Person = user;
            return View(profile);
        }

        public ActionResult Search(FormCollection searchBar)
        {
            string prefix = searchBar["user-input"];
            List<User> users = _userSerice.Search(prefix);
            return View(users);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SendFriendRequest(FriendRequest json)
        {
            FriendRequest friendRequest = new FriendRequest();
            friendRequest.FromUserId = User.Identity.GetUserId();
            friendRequest.ToUserId = json.ToUserId;
            _userSerice.SendFriendRequest(friendRequest);
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
			var manager = new UserManager<User>(new UserStore<User>(new ApplicationDbContext()));
			var currentUser = manager.FindById(User.Identity.GetUserId());
			currentUser.ProfilePic = newProfilePicUrl;
			manager.Update(currentUser);
			return View();
		}
        /*
		public ActionResult AddEmptyProfilePic() //óþarfi
		{
			var manager = new UserManager<User>(new UserStore<User>(new ApplicationDbContext()));
			var currentUser = manager.FindById(User.Identity.GetUserId());

			currentUser.ProfilePic = "/Content/Images/EmptyProfilePicture.gif";
			manager.Update(currentUser);
			return RedirectToAction("Index");
		}*/
    }
}
