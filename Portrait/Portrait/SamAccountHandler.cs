using System;
using System.IO;
using System.Web;

namespace Portrait
{
    public class SamAccountHandler : ThumbnailHandlerBase, IHttpHandler
    {
        protected override void InitSearchValues()
        {
            searchKey = Path.GetFileNameWithoutExtension(HttpContext.Current.Request.Path);
            searchTopic = "samaccountname";
            const string adFilter = "(&(objectClass=user)(objectCategory=person)(samaccountname={0}))";
            searchFilter = String.Format(adFilter, searchKey);
        }
    }
}
