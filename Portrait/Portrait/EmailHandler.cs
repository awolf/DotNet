using System;
using System.IO;
using System.Web;

namespace Portrait
{
    public class EmailHandler : ThumbnailHandlerBase, IHttpHandler
    {
        protected override void InitSearchValues()
        {
            searchKey = Path.GetFileNameWithoutExtension(HttpContext.Current.Request.Path);
            searchTopic = "Email";
            const string adFilter = "(&(objectClass=user)(objectCategory=person)(mail={0}@domain.com))";
            searchFilter = String.Format(adFilter, searchKey);
        }
    }
}
