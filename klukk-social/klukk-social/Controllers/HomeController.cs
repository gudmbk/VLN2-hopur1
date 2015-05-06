using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace klukk_social.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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
            test stuff;

            

            return View();
        }
    }

    /// <summary>
    /// Class used to be a test class
    /// for displaying test things in 
    /// a test enviroment.
    /// </summary>
    public class test
    {
        /// <summary>
        /// This function writes the parameters to the console
        /// </summary>
        /// <param name="number"></param>
        /// <param name="bla"></param>
        public void DoStuff(int number, string bla)
        {
            Console.WriteLine(number + bla);
        }
    }
}