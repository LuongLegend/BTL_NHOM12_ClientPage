using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_NHOM12_ClientPage.Models;
namespace BTL_NHOM12_ClientPage.Controllers
{
    public class ChinhSachHoanTienController : Controller
    {
        LinqDoNgoaiChinhHangDataContext db = new LinqDoNgoaiChinhHangDataContext();

        // GET: ChinhSachHoanTien
        public ActionResult Index()
        {
            var cate = from a in db.Categories select a;
            return View(cate);
        }
    }
}