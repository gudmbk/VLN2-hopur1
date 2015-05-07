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

        public ActionResult ParentHome()
        {

            return View();
        }

        public ActionResult ChildHome()
        {
            UserViewModel user = new UserViewModel();
            //user.Person = us.GetUserById(User.Identity.GetUserId();
            user.Feed = ps.GetAllPosts();
            return View();
        }
    }
}