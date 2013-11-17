using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Discuss
{
    public class DiscussRepository
    {

        DiscussDataContext data;

         public DiscussRepository()
         {
             data = new DiscussDataContext();

             var options = new System.Data.Linq.DataLoadOptions();
             options.LoadWith<Topic>(i => i.Messages);
             data.LoadOptions = options;
         }


        public Topic GetTopic(string uri)
        {

            var topic = data.Topics.Where(t => t.Uri == uri).SingleOrDefault();

            if (topic == null)
            {
                topic = new Topic { Uri = uri };

                data.Topics.InsertOnSubmit(topic);
                data.SubmitChanges();
            }

            return topic;
        }

        public void SaveMessage(Message message)
        {
            data.Messages.InsertOnSubmit(message);
            data.SubmitChanges();
        }
    }
}