using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bookstore.Controllers
{
    public class HomeController : Controller
    {
        //[LoginAuthorize(RoleNo ="")]
        [AllowAnonymous]
        public ActionResult Index()
        {
            vmHome model = new vmHome();
            return View(model);
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

        
    }
}