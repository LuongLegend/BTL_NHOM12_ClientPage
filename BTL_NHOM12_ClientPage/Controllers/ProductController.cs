using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_NHOM12_ClientPage.Models;
namespace BTL_NHOM12_ClientPage.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        LinqDoNgoaiChinhHangDataContext db = new LinqDoNgoaiChinhHangDataContext();
        Random random = new Random();
        List<ProductsModel> getTopProduct(int limit=10000)
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
            foreach(var item in listSale)
            {
                if (item.product_ID == product_ID)
                {
                    sale = item;
                    break;
                }
            }
            return sale;
        }
        ProductsModel getProductWithId(string id)
        {
            ProductsModel pro = new ProductsModel();
            var getProduct = from d in db.Products where (d.product_ID == id) && (d.active==true) select d;
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
        List<ProductsModel> getRelatingProduct(string id)
        {
           List<ProductsModel> listPro = new List<ProductsModel>();
            var getCategoryOfPro = from d in db.Products
                                join c in db.Product_Categories on d.product_ID equals c.product_ID
                                join a in db.Categories on c.category_ID equals a.category_ID
                                   where (c.product_ID==id)
                                select a;
            foreach(var item in getCategoryOfPro)
            {
                string group_ID = item.group_ID;
                var getProductWithGroupId = from a in db.Products
                                            join c in db.Product_Categories on a.product_ID equals c.product_ID
                                            join d in db.Categories on c.category_ID equals d.category_ID
                                            where (d.group_ID == group_ID) && (a.active == true)
                                            select a;
                foreach (var i in getProductWithGroupId)
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
                    if (listPro.Count == 5) break;
                }
                if (listPro.Count == 5) break;
            }
            return listPro;
        }
        int getProductBoughtNumber(string id)
        {
            int num = 0;
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
            foreach (var item in productsHadBought)
            {
                if (item.product_ID == id)
                {
                    return (int)item.amount;
                }
            }
            return num;
        }
        string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        bool checkDublicateKey(string id)
        {
            var a = from b in db.Contacts where b.contact_ID == id select b;
            return a.Count() > 0;
        }
        bool checkProduct(string id)
        {
            var a = from b in db.Products where b.product_ID == id select b;
            return a.Count() > 0;
        }
        public ActionResult Index(string id)
        {
            if(string.IsNullOrEmpty(id))
            Response.Redirect("/Product/All");
            
            ViewBag.allSale = getAllSale();
            ViewBag.sale = getSaleWithProductID(id);
            ViewBag.product = getProductWithId(id);
            ViewBag.topProduct = getTopProduct(8);
            ViewBag.relatingProduct = getRelatingProduct(id);
            ViewBag.boughtNumber = getProductBoughtNumber(id);
            ViewBag.productID = id;
            //get bonus of product
            var getBonus = from b in db.Bonus
                           join p in db.Product_Bonus on b.bonus_ID equals p.bonus_ID
                           where p.product_ID == id
                           select b;
            ViewBag.bonusCount = getBonus.Count();
            ViewBag.bonus = getBonus;
            return View();
        }
        void insertToContact(string id, string cusName, string phone, string time, string productName)
        {
            Contact a = new Contact();
            a.contact_ID = id;
            a.username = cusName;
            a.phone = phone;
            a.product_name = productName;
            a.status_Contact = "pending";
            a.create_date = Convert.ToDateTime(time);
            db.Contacts.InsertOnSubmit(a);
            db.SubmitChanges();
        }
        [HttpPost]
        public ActionResult Index(FormCollection f, string id)
        {
            string cusName =  f["subCusName"];
            string cusPhone =  f["subCusPhone"];
            string contactID = "CT" + RandomString(6);
            do
            {
                contactID = "CT" + RandomString(6);
            } while (checkDublicateKey(contactID));
            string time = DateTime.Now.ToString("dd/MM/yyyy");
            string productName = getProductWithId(id).product_name;
            insertToContact(contactID, cusName, cusPhone, time, productName);
            ViewBag.contacted = "true";
            //
            ViewBag.allSale = getAllSale();
            ViewBag.sale = getSaleWithProductID(id);
            ViewBag.product = getProductWithId(id);
            ViewBag.topProduct = getTopProduct(8);
            ViewBag.relatingProduct = getRelatingProduct(id);
            ViewBag.boughtNumber = getProductBoughtNumber(id);
            ViewBag.productID = id;
            //get bonus of product
            var getBonus = from b in db.Bonus
                           join p in db.Product_Bonus on b.bonus_ID equals p.bonus_ID
                           where p.product_ID == id
                           select b;
            ViewBag.bonusCount = getBonus.Count();
            ViewBag.bonus = getBonus;
            return View();
        }
        public ActionResult All()
        {
            List<ProductsModel> listPro = new List<ProductsModel>();
            var getCat = from c in db.Categories select c;
            ViewBag.sale = getAllSale();
            ViewBag.cates = getCat;
            var a = from pro in db.Products where (pro.active == true) select pro;
            foreach (var i in a)
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
            ViewBag.products = listPro;
            return View();
        }

    }
}