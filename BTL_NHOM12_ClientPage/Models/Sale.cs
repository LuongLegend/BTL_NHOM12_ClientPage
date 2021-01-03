using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_NHOM12_ClientPage.Models
{
    public class SaleModel
    {
        public string product_ID { get; set; }
        public string sale_ID { get; set; }
        public int min_product { get; set; }
        public int sale_price { get; set; }
    }
}