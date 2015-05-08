using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using klukk_social.Services;
using klukk_social.Models;

namespace klukk_social.Controllers
{
    public class HomeController : Controller
    {
		public ActionResult ResetServer()
		{
			IdentityManager manager = new IdentityManager();
			if (!manager.RoleExists("Parent"))
			{
				manager.CreateRole("Parent");
			}
			if (!manager.RoleExists("Child"))
			{
				manager.CreateRole("Child");
			}

			return View();
		}
        
        public ActionResult Index()
        {
			if (User.IsInRole("Parent"))
			{
				return RedirectToAction("ParentHome", "User");
			}
			if (User.IsInRole("Child"))
			{
				return RedirectToAction("ChildHome", "User");
			}
			return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Termo()
        {
            ViewBag.Message = "blablabal";
            return View();
        }
    }
}