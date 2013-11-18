using System;
using System.IO;
using System.Web;

namespace Portrait
{
    public class EmployeeNumberHandler : ThumbnailHandlerBase, IHttpHandler
    {
        protected override void InitSearchValues()
        {
            searchKey = Path.GetFileNameWithoutExtension(HttpContext.Current.Request.Path);
            searchTopic = "employeenumber";
            const string adFilter = "(&(objectClass=user)(objectCategory=person)(employeenumber={0}))";
            searchFilter = String.Format(adFilter, searchKey);
        }
    }
}
