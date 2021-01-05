using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_NHOM12_ClientPage.Models;
namespace BTL_NHOM12_ClientPage.Controllers
{
    public class ReviewNewsController : Controller
    {
        LinqDoNgoaiChinhHangDataContext db = new LinqDoNgoaiChinhHangDataContext();
        // GET: ReviewNews
        public ActionResult Index(string id)
        {
            var title = from a in db.News
                    where a.new_ID == id
                    select a;
            var cate = from a in db.Categories select a;
            ViewBag.cates = cate;
            //ViewBag.tt = title;
            return View(title);
        }
    }
}