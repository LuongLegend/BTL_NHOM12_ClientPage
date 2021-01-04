using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc;
using BTL_NHOM12_ClientPage.Models;

namespace BTL_NHOM12_ClientPage.Controllers
{
    public class BonusController : Controller
    {
        LinqDoNgoaiChinhHangDataContext db = new LinqDoNgoaiChinhHangDataContext();

        // GET: Bonus
        public ActionResult Index()
        {
            var list_bonus = from bonus in db.Bonus select bonus;
            ViewBag.list_bonus = list_bonus;

            return View();
        }
    }
}