using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_NHOM12_ClientPage.Models;
namespace BTL_NHOM12_ClientPage.Controllers
{
    public class SearchProductController : Controller
    {
        // GET: SearchProduct
        LinqDoNgoaiChinhHangDataContext db = new LinqDoNgoaiChinhHangDataContext();
        public ActionResult Index()
        {
            string query = Request["tk"];
            List<ProductsModel> listPro = new List<ProductsModel>();
            List<SaleModel> listSale = new List<SaleModel>();
            var getAllSale = from s in db.Sales
                             join b in db.Product_Sales on s.sale_ID equals b.sale_ID
                             select new
                             {
                                 product_ID = b.product_ID,
                                 sale_ID = s.sale_ID,
                                 min_product = s.min_product,
                                 sale_price = s.sale_price
                             };
            foreach (var item in getAllSale)
            {
                SaleModel s = new SaleModel();
                s.sale_ID = item.sale_ID;
                s.sale_price = (int)item.sale_price;
                s.product_ID = item.product_ID;
                s.min_product = (int)item.min_product;
                listSale.Add(s);
            }
            var getCat = from c in db.Categories select c;
            ViewBag.sale = listSale;
            ViewBag.query = query;
            ViewBag.cates = getCat;
            var a = from pro in db.Products
                    where pro.product_name.Contains(query)
                         || pro.product_brand.Contains(query)
                         || pro.origin.Contains(query)
                    select pro;
            foreach(var i in a)
            {
                ProductsModel pro = new ProductsModel();
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
                listPro.Add(pro);
            }
            ViewBag.count = a.Count();
            ViewBag.products = listPro;
            return View();
        }
    }
}