using System.Web;
using System.Web.Mvc;

namespace BTL_NHOM12_ClientPage
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
