using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portrait
{
    public class CacheDisplay : IHttpHandler
    {

        long sizeOfCache = 0;
        int countOfCache = 0;
        int countOfImages = 0;

        public void ProcessRequest(HttpContext context)
        {

            context.Response.Write(@"$$$$$$$\                       $$\                        $$\   $$\     ");
            context.Response.Write("\n");
            context.Response.Write(@"$$  __$$\                      $$ |                       \__|  $$ |    ");
            context.Response.Write("\n");
            context.Response.Write(@"$$ |  $$ | $$$$$$\   $$$$$$\ $$$$$$\    $$$$$$\  $$$$$$\  $$\ $$$$$$\   ");
            context.Response.Write("\n");
            context.Response.Write(@"$$ |  $$ | $$$$$$\   $$$$$$\ $$$$$$\    $$$$$$\  $$$$$$\  $$\ $$$$$$\   ");
            context.Response.Write("\n");
            context.Response.Write(@"$$$$$$$  |$$  __$$\ $$  __$$\\_$$  _|  $$  __$$\ \____$$\ $$ |\_$$  _|  ");
            context.Response.Write("\n");
            context.Response.Write(@"$$  ____/ $$ /  $$ |$$ |  \__| $$ |    $$ |  \__|$$$$$$$ |$$ |  $$ |    ");
            context.Response.Write("\n");
            context.Response.Write(@"$$ |      $$ |  $$ |$$ |       $$ |$$\ $$ |     $$  __$$ |$$ |  $$ |$$\ ");
            context.Response.Write("\n");
            context.Response.Write(@"$$ |      \$$$$$$  |$$ |       \$$$$  |$$ |     \$$$$$$$ |$$ |  \$$$$  |");
            context.Response.Write("\n");
            context.Response.Write(@"\__|       \______/ \__|        \____/ \__|      \_______|\__|   \____/ ");
            context.Response.Write("\n\n\n");

            context.Response.ContentType = "text/plain";

            foreach (System.Collections.DictionaryEntry item in context.Cache)
            {
                context.Response.Write(item.Key + "\n");
                TallyCounts(item);
            }
            context.Response.Write("\n\n\n");
            context.Response.Write("*********** CACHE ************************\n");
            context.Response.Write(String.Format("Total Items:                      {0} \n", countOfCache));
            context.Response.Write(String.Format("Image Count:                      {0} \n", countOfImages));
            context.Response.Write(String.Format("Image Memory Used:                {0} kb \n", (sizeOfCache / 1000)));
            context.Response.Write("******************************************\n");

        }

        private void TallyCounts(System.Collections.DictionaryEntry item)
        {
            string key = item.Key as string;
            countOfCache++;

            if (!key.Contains("NoImage") && (key.Contains("Email") || key.Contains("employeenumber") || key.Contains("samaccountname")))
            {
                countOfImages++;
                try
                {
                    sizeOfCache += ((byte[])item.Value).Length;
                }
                catch (Exception) { }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
