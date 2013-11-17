using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Discuss
{
    public partial class Default : System.Web.UI.Page
    {
        private Topic topic;

        public string Referrer
        {
            get { return OriginalReferrer.Text; }
            set { OriginalReferrer.Text = value; }
        }

        public Guid TopicId
        {
            get { return new Guid(OriginalTopicId.Text); }
            set { OriginalTopicId.Text = value.ToString(); }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Submit.Click += (s, ev) => SaveMessage();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.UrlReferrer == null) Response.Redirect("NoReferrer.aspx");
                SaveReferrer();
                InitTopic();
            }

            if (Request.UrlReferrer != null)
            {
                Output.Text = "The referrer is " + Request.UrlReferrer.AbsoluteUri;
            }
        }

        private void SaveReferrer()
        {
            if (Request.UrlReferrer != null)
            {
                Referrer = Request.UrlReferrer.AbsoluteUri;
            }
        }

        private void InitTopic()
        {
            LoadTopic();
            
            TopicId = topic.Id;
            DisplayTopic();
        }

        private void SaveMessage()
        {
            if (string.IsNullOrEmpty(Content.Text)) return;

            var data = new DiscussRepository();

            data.SaveMessage(new Message { Content = Content.Text, TopicId = TopicId, Sender = "a184670" });

            Content.Text = string.Empty;

            LoadTopic();
            DisplayTopic();
        }

        private void LoadTopic()
        {
            var data = new DiscussRepository();

            topic = data.GetTopic(Referrer);

        }

        private void DisplayTopic()
        {
            var builder = new StringBuilder();

            var template = @"
                <tr>
                    <td>
                        <img src='images/basic2-101.png' />
                    </td>
                    <td>
                        <div class='contnt'>{0}</div>
                    </td>
                </tr>
                <tr>
                    <td colspan='2' >
                        <div class='time'><abbr data-livestamp='{1}'></abbr> by {2}</div>
                    </td>
                </tr>
                <tr><td colspan='2'><hr></td></tr>";


            foreach (var message in topic.Messages.OrderBy(o => o.Created))
            {

                var dt = new DateTime(message.Created.Ticks, DateTimeKind.Utc);
                builder.Append(string.Format(template, message.Content, message.Created.ToLocalTime().ToString("o"), message.Sender));
            }

            Messages.Text = builder.ToString();

        }
    }
}