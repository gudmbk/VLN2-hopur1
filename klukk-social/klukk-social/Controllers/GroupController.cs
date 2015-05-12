using klukk_social.Models;
using System.Web.Mvc;

namespace klukk_social.Controllers
{
    public class GroupController : Controller
    {
        public ActionResult CreateGroup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateGroup(FormCollection collection)
        {

            return null;
        }
		public ActionResult Index()
		{
			GroupViewModel Bag = new GroupViewModel();
			return View();
		}
    }
}