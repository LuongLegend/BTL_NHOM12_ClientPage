using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BTL_NHOM12_ClientPage
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            System.IO.StreamReader sr;
            sr = new System.IO.StreamReader(Server.MapPath("~/totalVisitedUser.txt"));
            string numUser = sr.ReadLine();
            sr.Close();
            Application["totalVisitedUser"] = numUser;
            Application["currentUser"] = "0";
        }

        protected void Session_Start()
        {
            HttpContext.Current.Application["currentUser"] = int.Parse(HttpContext.Current.Application["currentUser"].ToString()) + 1;
            HttpContext.Current.Application["totalVisitedUser"] = int.Parse(HttpContext.Current.Application["totalVisitedUser"].ToString()) + 1;
            System.IO.StreamWriter sw;
            sw = new System.IO.StreamWriter(Server.MapPath("totalVisitedUser.txt"));
            sw.Write(HttpContext.Current.Application["totalVisitedUser"].ToString());
            sw.Close();
        }
    }
}
