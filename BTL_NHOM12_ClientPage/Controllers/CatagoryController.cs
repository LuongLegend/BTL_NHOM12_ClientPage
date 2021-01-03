using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_NHOM12_ClientPage.Controllers
{
    public class CatagoryController : Controller
    {
        // GET: Catagory
        public ActionResult Index(string id="1")
        {
            string z = Request["cat"];
            ViewBag.idd  = id;
            ViewBag.cat = z;
            return View();
        }
    }
}