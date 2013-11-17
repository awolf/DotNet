using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Pixel
{
    public class Image : IHttpHandler
    {
        private string connString;

        public Image()
        {
            connString = ConfigurationManager.ConnectionStrings["WebData"].ConnectionString;
        }
        
        public void ProcessRequest(HttpContext context)
        {
            var serverData = context.Request.ServerVariables;
            context.Response.ContentType = "image/gif";
            context.Response.WriteFile(context.Server.MapPath("one-pixel.gif"));

            if (serverData["HTTP_REFERER"] == null) return;
            
            IDbConnection connection = new SqlConnection(connString);
            try
            {
                connection.Execute(
                        @"INSERT INTO [Requests] 
                            ([Url],[Query],[TrackingCode],[UserAgent],[UserId]) 
                        VALUES 
                            (@Url, @Query ,@TrackingCode, @UserAgent ,@UserId)",
                    new
                    {
                        Url = serverData["HTTP_REFERER"],
                        Query = context.Request.Url.Query,
                        TrackingCode = GetTrackingCode(context),
                        UserAgent = serverData["HTTP_USER_AGENT"],
                        UserId = serverData["LOGON_USER"]
                    }
                );
            }
            finally
            {
                connection.Close();
            }
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        private int GetTrackingCode(HttpContext context)
        {
            var code = 0;
            int.TryParse(context.Request.QueryString["TrackingCode"], out code);
            return code;
        }
    }
}