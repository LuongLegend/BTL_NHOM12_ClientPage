using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_NHOM12_ClientPage.Models;
namespace BTL_NHOM12_ClientPage.Controllers
{
    public class HomeController : Controller
    {
        LinqDoNgoaiChinhHangDataContext db = new LinqDoNgoaiChinhHangDataContext();
        List<ProductsModel> getTopProduct(int limit = 10000)
        {
            List<ProductsModel> listTopPro = new List<ProductsModel>();
            //get list amount products had bought - return productID and amount
            var productsHadBought = from Product_Bill in db.Product_Bills
                                    join B in db.Bills on Product_Bill.bill_ID equals B.bill_ID
                                    join C in db.Products on Product_Bill.product_ID equals C.product_ID
                                    where
                                      B.status_bill == "approved" &&
                                      C.active == true
                                    group Product_Bill by new
                                    {
                                        Product_Bill.product_ID
                                    } into g
                                    orderby
                                      (int?)g.Sum(p => p.quanitity) descending
                                    select new
                                    {
                                        g.Key.product_ID,
                                        amount = (int?)g.Sum(p => p.quanitity)
                                    };
            var allProduct = from a in db.Products select a;

            foreach (var item in productsHadBought)
            {
                foreach (var i in allProduct)
                {
                    if (item.product_ID == i.product_ID)
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
                        listTopPro.Add(pro);
                        if (listTopPro.Count == limit) break;
                    }
                    if (listTopPro.Count == limit) break;
                }
            }
            return listTopPro;
        }
        public ActionResult Index()
        {
            List<ProductsModel> listTopPro = new List<ProductsModel>();
            List<SaleModel> listSale = new List<SaleModel>();
            var getAllCat = from a in db.Categories select a;
            ViewBag.Cat = getAllCat;
            var getProductMyPhamDacTri = from a in db.Products
                                         join c in db.Product_Categories on a.product_ID equals c.product_ID
                                         join d in db.Categories on c.category_ID equals d.category_ID
                                         where (d.group_ID == "1") && (a.active == true)
                                         select a;
            var getProductLongMy = from a in db.Products
                                   join c in db.Product_Categories on a.product_ID equals c.product_ID
                                   join d in db.Categories on c.category_ID equals d.category_ID
                                   where (d.group_ID == "4") && (a.active == true)
                                   select a;
            var getProductChamSocDa = from a in db.Products
                                   join c in db.Product_Categories on a.product_ID equals c.product_ID
                                   join d in db.Categories on c.category_ID equals d.category_ID
                                   where (d.group_ID == "3") && (a.active == true)
                                   select a;
            var getProductThucPhamChucNang = from a in db.Products
                                    join c in db.Product_Categories on a.product_ID equals c.product_ID
                                    join d in db.Categories on c.category_ID equals d.category_ID
                                    where (d.group_ID == "2") && (a.active == true)
                                    select a;
            var getProductCollagen = from a in db.Products
                                             join c in db.Product_Categories on a.product_ID equals c.product_ID
                                             join d in db.Categories on c.category_ID equals d.category_ID
                                             where (d.group_ID == "7") && (a.active == true)
                                             select a;
            var getAllSale = from a in db.Sales
                             join b in db.Product_Sales on a.sale_ID equals b.sale_ID
                             select new
                             {
                                 product_ID = b.product_ID,
                                 sale_ID = a.sale_ID,
                                 min_product = a.min_product,
                                 sale_price = a.sale_price
                             };
            foreach(var item in getAllSale)
            {
                SaleModel s = new SaleModel();
                s.sale_ID = item.sale_ID;
                s.sale_price = (int)item.sale_price;
                s.product_ID = item.product_ID;
                s.min_product = (int)item.min_product;
                listSale.Add(s);
            }
            //var getAllPro = from a in db.Products select a;
            ViewBag.sale = listSale;
            ViewBag.MyPhamDacTri = getProductMyPhamDacTri;
            ViewBag.LongMy = getProductLongMy;
            ViewBag.ChamSocDa = getProductChamSocDa;
            ViewBag.Collagen = getProductCollagen;
            ViewBag.TPCN = getProductThucPhamChucNang;
            ViewBag.TopProduct = getTopProduct(8);
            return View();
        }
        public ActionResult About()
        {
            var getAllCat = from a in db.Categories select a;
            ViewBag.cates = getAllCat;
            return View();
        }
        
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}