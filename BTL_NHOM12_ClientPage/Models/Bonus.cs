using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_NHOM12_ClientPage.Models
{
    public class BonusModel
    {
        public string bonusID { get; set; }
        public string name { get; set; }
        public string photo { get; set; }
        public int price { get; set; }
        public List<ProductsModel> listProBonus { get; set; }
    }
}