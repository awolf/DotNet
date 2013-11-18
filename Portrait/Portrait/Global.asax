using System;
using System.IO;
using System.Web;

namespace Portrait
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            string filepath = HttpContext.Current.Server.MapPath("~/default.jpg");

            this.Context.Cache.Insert("GenericUserImage", 
                File.ReadAllBytes(filepath),
                new System.Web.Caching.CacheDependency(filepath));
        }
    }
}
