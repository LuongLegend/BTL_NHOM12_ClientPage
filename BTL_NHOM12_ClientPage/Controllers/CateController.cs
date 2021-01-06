using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_NHOM12_ClientPage.Models;
namespace BTL_NHOM12_ClientPage.Controllers
{
    public class CateController : Controller
    {
        // GET: Cate // Cate?cat=1,23, Cate/Index/cat12(id)
        LinqDoNgoaiChinhHangDataContext db = new LinqDoNgoaiChinhHangDataContext();
        //DataClasses1DataContext ss = new DataClasses1DataContext();
        public ActionResult Index(string id="!!!")
        {
            List<KeyValuePair<string, string>> defGroup = new List<KeyValuePair<string, string>>();
            defGroup.Add(new KeyValuePair<string, string>("1","Mỹ phẩm Đặc Trị"));
            defGroup.Add(new KeyValuePair<string, string>("2", "Thực phẩm Chức Năng"));
            defGroup.Add(new KeyValuePair<string, string>("3", "Mỹ phẩm Chăm sóc Da"));
            defGroup.Add(new KeyValuePair<string, string>("4", "Thuốc mọc Tóc, Râu, Mi, Mày"));
            defGroup.Add(new KeyValuePair<string, string>("7", "Collagen Dẹp da"));
            defGroup.Add(new KeyValuePair<string, string>("6", "Nước hoa Chính Hãng"));
            defGroup.Add(new KeyValuePair<string, string>("8", "Thương hiệu hàng đầu"));
            string cat = Request["cat"];
            ViewBag.nameCat = "";
            ViewBag.idd = id;
            if (id != "!!!") //find by ID
            {
                var getCat = from a in db.Categories 
                             where a.category_ID == id 
                    select a;
                ViewBag.catDetail = getCat;
                var getProOfCat = from a in db.Products
                                  join b in db.Product_Categories on a.product_ID equals b.product_ID
                                  join c in db.Categories on b.category_ID equals c.category_ID
                                  where c.category_ID==id
                                  select a;
                ViewBag.Products = getProOfCat;
            }
            else
            {
                string nameCat = "";
                foreach(var item in defGroup)
                {
                    if (item.Key == cat) nameCat = item.Value;
                }
                ViewBag.nameCat = nameCat;
              
                var getProOfCat = from a in db.Products
                                  join b in db.Product_Categories on a.product_ID equals b.product_ID
                                  join c in db.Categories on b.category_ID equals c.category_ID
                                  where c.group_ID == cat
                                  select a;
                ViewBag.Products = getProOfCat;
            }
            ViewBag.idd = id;
            ViewBag.cat = cat;
            return View();
        }
    }
}