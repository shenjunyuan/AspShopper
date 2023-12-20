using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bookstore.Controllers
{
    public class CustomerController : Controller
    {
        [SecurityAuthorize(SecurityMode = enumSecuritys.Browse)]
        public ActionResult Index()
        {
            SessionService.ActionTips = "資料列表";
            return View();
        }
    }
}