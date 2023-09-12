namespace Alpha.HowToUse
{
    using AlphaPlus.User.Models;
    using System.Web;
    using System.Web.UI;

    public partial class Content_Page : Fanex.Age.UI.TplPage
    {
        // Folder Name     
        private const string AccInfo = "AccInfo";

        public override void ProcessRequest(ref System.Collections.Hashtable p)
        {
        }

        public override object ValidateRequest(System.Collections.Hashtable p)
        {
            return null;
        }

        public override object InvokeData(System.Collections.Hashtable p)
        {
            return null;
        }

        public override void RenderContent(HtmlTextWriter writer)
        {
            Content_Template tpl = new Content_Template();
            string folderName = Request.QueryString["folder"].ToString();

            // Encrypt paras to Cache for Flash
            string parameter = string.Empty;
            parameter = string.Format("{0}&{1}&{2}", folderName, UserSession.Current.RoleId, UserSession.Current.Language);
            parameter = Sunplus.Agent.Utilities.Encryption.CfsEnCrypt(parameter);
            tpl.source = string.Format("{0}Handlers/Flash.ashx?params={1}", GetBaseUrl(), parameter);

            tpl.width = Server.HtmlEncode(Request.QueryString["fw"]);
            tpl.height = Server.HtmlEncode(Request.QueryString["fh"]);

            tpl.title = GetPageTitle(folderName);

            writer.Write(tpl);
        }

        private string GetPageTitle(string folderName)
        {
            var title = string.Empty;
            switch (folderName)
            {               
                case AccInfo:
                    title = LocalResources.HowToUse.AccInfoFunction;
                    break;  
                default:
                    break;
            }
            return title;
        }

        private string GetBaseUrl()
        {
            string UrlSlash = "/";
            string hostName = HttpContext.Current.Request.Headers["Host"];
            string appPath = HttpContext.Current.Request.ApplicationPath;
            string url = string.Format("//{0}{1}", hostName, appPath);

            if (appPath.Length > 1)
            {
                url += UrlSlash;
            }

            return url;
        }

    }
}