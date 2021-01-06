using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_NHOM12_ClientPage.Models;
namespace BTL_NHOM12_ClientPage.Controllers
{
    public class CartController : Controller
    {
        LinqDoNgoaiChinhHangDataContext db = new LinqDoNgoaiChinhHangDataContext();
        List<SaleModel> getAllSale()
        {
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
            return listSale;
        }
        SaleModel getSaleWithProductID(string product_ID)
        {
            List<SaleModel> listSale = getAllSale();
            SaleModel sale = new SaleModel();
            foreach (var item in listSale)
            {
                if (item.product_ID == product_ID)
                {
                    sale = item;
                    break;
                }
            }
            return sale;
        }
        bool checkProductId(string productID)
        {
            var record = db.Products.Where(item => item.product_ID == productID).FirstOrDefault();
            if (record != null) return true;
            else return false;
        }
        CartItem createCart(string productId, int quantity)
        {
            var record = db.Products.Where(item => item.product_ID == productId).FirstOrDefault();
            CartItem i = new CartItem();
            i.productID = productId;
            i.quantity = quantity;
            i.productName = record.product_name;
            i.price = (int)record.price;
            i.photo = record.photo;
            //bonus
            List<string> listBonusName = new List<string>();
            var getBonus = from b in db.Bonus
                            join p in db.Product_Bonus on b.bonus_ID equals p.bonus_ID
                            where p.product_ID == productId
                            select b;
            foreach(var item in getBonus)
            {
                listBonusName.Add(item.name_bonus);
            }
            i.bonus = listBonusName;
            //sale
            SaleModel s = getSaleWithProductID(productId);
            i.min_product = s.min_product;
            i.sale_price = s.sale_price;
            return i;
        }
        // GET: Cart
        public ActionResult Index()
        {
            List<CartItem> Cart = new List<CartItem>();
            if (Session["Cart"] == null)
                Session["Cart"] = Cart;
            else
                Cart = Session["Cart"] as List<CartItem>;
            ViewBag.countPro = Cart.Count();
            return View(Cart);
        }
        public ActionResult Add(string productId, int quantity=1)
        {
            // lay ban ghi tuong ung voi id
            if (checkProductId(productId))
            {
                ShoppingCart c = new ShoppingCart();
                CartItem newCart =  createCart(productId, quantity);
                c.CartAdd(newCart);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Remove(string productId)
        {
            ShoppingCart objCart = new ShoppingCart();
            objCart.CartDelete(productId);
            return RedirectToAction("Index");
        }
    }
}