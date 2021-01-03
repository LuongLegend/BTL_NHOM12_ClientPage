using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace BTL_NHOM12_ClientPage.Models
{
    public class ProductsModel
    {
        public string product_ID { get; set; }
        public string product_name { get; set; }
        public string product_brand { get; set; }
        public string origin { get; set; }
        public string summary { get; set; }
        public bool status_product { get; set; }
        public int price { get; set; }
        public int quantity   { get; set; }
        public string photo { get; set; }
        public string detail { get; set; }
        public bool active { get; set; }
    }
}