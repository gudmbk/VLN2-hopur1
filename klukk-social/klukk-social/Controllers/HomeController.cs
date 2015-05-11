using System.Web.Mvc;

namespace klukk_social.Controllers
{
    public class HomeController : Controller
    {
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
       
        public ActionResult Terms()
        {
            return View();
        }

        public ActionResult WhySafe()
        {
            return View();
        }

        public ActionResult ForPar()
        {
            return View();
        }

    }
}