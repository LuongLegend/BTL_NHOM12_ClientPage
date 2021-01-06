using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_NHOM12_ClientPage.Models;
namespace BTL_NHOM12_ClientPage.Controllers
{
    public class CatagoryController : Controller
    {
        LinqDoNgoaiChinhHangDataContext db = new LinqDoNgoaiChinhHangDataContext();
        // GET: Catagory
        string getCatName(string groupId)
        {
            string catName = "";
            switch (groupId)
            {
                case "1": catName = "Mỹ phẩm Đặc Trị"; break;
                case "2": catName = "Thực phẩm Chức Năng"; break;
                case "3": catName = "Mỹ phẩm Chăm sóc Da"; break;
                case "4": catName = "Thuốc mọc Tóc, Râu, Mi, Mày"; break;
                case "6": catName = "Nước hoa Chính Hãng"; break;
                case "7": catName = "Collagen Dẹp da"; break;
                case "8": catName = "Thương hiệu hàng đầu"; break;
            }

            return catName;
        }
        public ActionResult Index(string id="~~~")
        {
            var cate = from a in db.Categories select a;
            ViewBag.cates = cate;
            List<SaleModel> listSale = new List<SaleModel>();
            var getAllSale = from a in db.Sales
                             join b in db.Product_Sales on a.sale_ID equals b.sale_ID
                             select new
                             {
                                 product_ID = b.product_ID,
                                 sale_ID = a.sale_ID,
                                 min_product = a.min_product,
                                 sale_price = a.sale_price
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
            ViewBag.sale = listSale;
            if (id != "~~~") //find by category_ID
            {
                var CatRecord = db.Categories.Where(item => item.category_ID == id).FirstOrDefault();
                ViewBag.name = CatRecord.category_name;
                ViewBag.description = CatRecord.description;
                var getProducts = from a in db.Product_Categories
                                  join b in db.Products on a.product_ID equals b.product_ID
                                  join c in db.Categories on a.category_ID equals c.category_ID
                                  where c.category_ID == id && b.active == true
                                  select b;
                ViewBag.products = getProducts;
                ViewBag.catID = true;
            }
            else // find by group ID
            {
                string z = Request["cat"];
                var getProducts = from a in db.Product_Categories
                                join b in db.Products on  a.product_ID equals b.product_ID
                                join c in db.Categories on a.category_ID equals c.category_ID
                                where c.group_ID==z && b.active==true
                                select b;
                string catName = getCatName(z);
                ViewBag.name = catName;
                ViewBag.products = getProducts;
                ViewBag.catID = false;
            }
            ViewBag.idd  = id;
            return View();
        }
    }
}