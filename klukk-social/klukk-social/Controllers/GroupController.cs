using klukk_social.Models;
using klukk_social.Services;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

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
			GroupService _groupService = new GroupService();
			GroupViewModel bag = new GroupViewModel();
			bag.GroupList = _groupService.GetAllGroups(User.Identity.GetUserId());
			return View(bag);
		}
		public ActionResult OwnedGroups()
		{
			return View();
		}
    }
}