using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using klukk_social.Models;
using klukk_social.Services;

namespace klukk_social.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public ActionResult ParentHome()
        {
            PostService ps = new PostService();
            List<Post> list = ps.GetAllPosts();
            return View();
        }

        public ActionResult ChildHome()
        {
            return View();
        }
    }
}