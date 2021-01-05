using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_NHOM12_ClientPage.Models;
namespace BTL_NHOM12_ClientPage.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        LinqDoNgoaiChinhHangDataContext db = new LinqDoNgoaiChinhHangDataContext();
        Random random = new Random();
        string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        ProductsModel getProductWithId(string id)
        {
            ProductsModel pro = new ProductsModel();
            var getProduct = from d in db.Products where (d.product_ID == id) && (d.active == true) select d;
            foreach (var i in getProduct)
            {
                pro.product_ID = i.product_ID;
                pro.product_name = i.product_name;
                pro.product_brand = i.product_brand;
                pro.origin = i.origin;
                pro.photo = i.photo;
                pro.price = (int)i.price;
                pro.quantity = (int)i.quantity;
                pro.status_product = (bool)i.status_product;
                pro.summary = i.summary;
                pro.active = (bool)i.active;
                pro.detail = i.detail;
                break;
            }
            return pro;
        }
        [HttpPost]
        public ActionResult Index()
        {
            string name = Request["inputName"];
            string phone = Request["inputPhone"];
            string address = Request["inputAddress"];
            string note = Request["inputNote"];
            string mode = Request["mode"];
            string productID = Request["productID"];
            string time = DateTime.Now.ToString("dd/MM/yyyy");
            ViewBag.time = time;
            ViewBag.name = name;
            ViewBag.phone = phone;
            ViewBag.address = address;
            ViewBag.note = note;
            ViewBag.mode = mode;
            ViewBag.madh = "DH"+RandomString(6);
            if (mode == "onePro")
            {
                ViewBag.product = getProductWithId(productID);
                var getBonus = from b in db.Bonus
                               join p in db.Product_Bonus on b.bonus_ID equals p.bonus_ID
                               where p.product_ID == productID
                               select b;
                ViewBag.bonus = getBonus;
            }
            return View();
        }
    }
}