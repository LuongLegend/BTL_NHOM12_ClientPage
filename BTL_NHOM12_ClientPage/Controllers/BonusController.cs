using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_NHOM12_ClientPage.Models;
namespace BTL_NHOM12_ClientPage.Controllers
{

    public class BonusController : Controller
    {
        LinqDoNgoaiChinhHangDataContext db = new LinqDoNgoaiChinhHangDataContext();
        // GET: Bonus
        List<ProductsModel> getProductWithBonusId(string bonusID, int limit = 100000)
        {
            List<ProductsModel> listPro = new List<ProductsModel>();
            var getProWithBonusID = from b in db.Bonus
                                    join a in db.Product_Bonus on b.bonus_ID equals a.bonus_ID
                                    join p in db.Products on a.product_ID equals p.product_ID
                                    where b.bonus_ID == bonusID && p.active ==true
                                    select p;
            foreach(var i in getProWithBonusID)
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
                if (listPro.Count == limit) break;
            }
            return listPro;
        }
        public ActionResult Index()
        {
            var bonus = from a in db.Bonus select a;
            List<BonusModel> listBonus = new List<BonusModel>();
            foreach(var item in bonus)
            {
                BonusModel bn = new BonusModel();
                bn.bonusID = item.bonus_ID;
                bn.name = item.name_bonus;
                bn.price = (int)item.price;
                bn.photo = item.photo;
                List<ProductsModel> listPro = getProductWithBonusId(item.bonus_ID, 6);
                bn.listProBonus = listPro;
                listBonus.Add(bn);
            }
            ViewBag.bonus = listBonus;
            return View();
        }
        public ActionResult ViewBonus(string id)
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
            //getName
            var c = db.Bonus.Where(i => i.bonus_ID == id).SingleOrDefault();
            ViewBag.name = c.name_bonus;
            List<ProductsModel> pro = getProductWithBonusId(id);
            ViewBag.products = pro;
            return View();
        }
    }
}