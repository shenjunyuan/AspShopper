using ECPay.Payment.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bookstore.Controllers
{
    public class HomeController : Controller
    {
        //[LoginAuthorize(RoleNo ="admin")]
        [AllowAnonymous]
        public ActionResult Index()
        {
            vmHome model = new vmHome();
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult About()
        {

            vmHome model = new vmHome();
            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
    }
}