using System;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Web;
using System.Web.Caching;

namespace Portrait
{
    public abstract class ThumbnailHandlerBase
    {
        protected string searchKey = string.Empty;
        protected string searchTopic = string.Empty;
        protected string searchFilter = string.Empty;

        protected abstract void InitSearchValues();

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            WriteResponseCachePolicy();

            WriteNotModifiedResponse();

            InitSearchValues();

            ServeThumbnail();
        }

        private static void WriteResponseCachePolicy()
        {
            var cache = HttpContext.Current.Response.Cache;

            cache.SetCacheability(HttpCacheability.Public);
            cache.SetLastModified(DateTime.Now);
            cache.SetExpires(DateTime.Now.AddDays(7));
            cache.SetMaxAge(TimeSpan.FromDays(7));
            cache.SetValidUntilExpires(true);
        }

        private void WriteNotModifiedResponse()
        {
            var modifiedHeader = HttpContext.Current.Request.Headers["If-Modified-Since"];
            var response = HttpContext.Current.Response;

            if (!String.IsNullOrEmpty(modifiedHeader))
            {
                var modifiedSince = DateTime.Parse(modifiedHeader).ToUniversalTime();
                var modifieddiff = DateTime.UtcNow.Subtract(modifiedSince);

                if (modifieddiff.TotalDays < 7)
                {
                    response.StatusCode = 304;
                    response.StatusDescription = "Not Modified";
                    response.AddHeader("Content-Length", "0");
                    response.End();
                }
            }
        }

        private void ServeThumbnail()
        {
            var response = HttpContext.Current.Response;

            response.ContentType = "image/jpeg";
            response.BinaryWrite(GetThumbNailImage());
            response.End();
        }

        private byte[] GetThumbNailImage()
        {
            byte[] img = null;

            img = GetThumbnailImageFromCache();

            if (img == null)
            {
                img = GetADThumbnailPhoto();
            }

            if (img == null)
            {
                img = GetGenericUserImage();
            }

            return img;
        }

        private byte[] GetGenericUserImage()
        {
            return (byte[])HttpContext.Current.Cache["GenericUserImage"];
        }

        private byte[] GetThumbnailImageFromCache()
        {
            var img = (byte[])HttpContext.Current.Cache[String.Format("{0}-{1}", searchKey, searchTopic)];

            if (img == null)
            {
                if (HttpContext.Current.Cache[String.Format("{0}-NoImage-{1}", searchKey, searchTopic)] != null)
                {
                    img = GetGenericUserImage();
                }
            }
            return img;
        }

        private byte[] GetADThumbnailPhoto()
        {
            byte[] img = null;

            using (DirectoryEntry rootEntry = Domain.GetCurrentDomain().GetDirectoryEntry())
            {
                using (DirectorySearcher search = new DirectorySearcher(rootEntry))
                {
                    search.CacheResults = false;
                    search.ServerTimeLimit = TimeSpan.FromSeconds(5);
                    search.ServerPageTimeLimit = TimeSpan.FromSeconds(5);
                    search.ClientTimeout = TimeSpan.FromSeconds(15);
                    search.PageSize = 30;
                    search.Filter = searchFilter;
                    search.PropertiesToLoad.Add("thumbnailPhoto");
                    search.SearchScope = SearchScope.Subtree;

                    SearchResult user = search.FindOne();
                    if (user != null)
                    {
                        var tp = user.Properties["thumbnailPhoto"];
                        if (tp != null)
                        {
                            if (tp.Count > 0)
                            {
                                img = (byte[])tp[0];
                                if (img != null)
                                {
                                    HttpContext.Current.Cache.Insert(String.Format("{0}-{1}", searchKey, searchTopic), img, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(10));
                                }
                            }
                        }
                    }
                }
            }

            if (img == null)
            {
                HttpContext.Current.Cache.Insert(String.Format("{0}-NoImage-{1}", searchKey, searchTopic), "", null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(10));
            }

            return img;
        }
    }
}
