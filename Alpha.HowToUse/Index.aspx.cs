namespace Alpha.HowToUse
{
    using System.Web.Security;
    using Sunplus.Agent.Model.Entities;

    public partial class Index_Page : Fanex.Age.UI.TplPage
    {
        private string mainSource = string.Empty;

        public override void ProcessRequest(ref System.Collections.Hashtable p)
        {
            if (UserSession.Current.CustId == 0)
            {
                Response.Redirect(FormsAuthentication.LoginUrl, true);

                return;
            }

            string url = Request["src"];
            if (!string.IsNullOrEmpty(url))
            {
                string parameter = url + string.Empty;
                parameter = parameter.Replace(' ', '+');
                parameter = Sunplus.Agent.Utilities.Encryption.CfsDeCrypt(parameter);
                mainSource = "Content.aspx?" + parameter;
            }
            else if (Request.QueryString["lang"] != null)
            {
                mainSource = string.Format("{0}/Resources/HTML/WinLimit/WinLimit_{1}.html", Request.ApplicationPath, CurrentUserSession.Language);
            }
            else
            {
                mainSource = string.Format("{0}/Intro/Intro_{1}.html", Request.ApplicationPath, CurrentUserSession.Language);
            }
        }

        public override object ValidateRequest(System.Collections.Hashtable p)
        {
            return null;
        }

        public override object InvokeData(System.Collections.Hashtable p)
        {
            return null;
        }

        public override void RenderContent(System.Web.UI.HtmlTextWriter writer)
        {            
            if (!UserSession.Current.Site.Equals("789y", System.StringComparison.OrdinalIgnoreCase))
            {
                Index_Template tpl = new Index_Template();
                tpl.mainFrameSrc = mainSource;
                writer.Write(tpl);
            }
            else
            {
                writer.Write("<h1>This function is under construction</h1>");
            }
        }
    }
}