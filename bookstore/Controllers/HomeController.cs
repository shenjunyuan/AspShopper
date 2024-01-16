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

            var items = model.NewItemData;
            foreach (var item in items)
            {
                string bname = item.category_name;
                Console.WriteLine(bname);
            }
            return View(model);
        }

        public ActionResult About()
        {


            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
    }
}