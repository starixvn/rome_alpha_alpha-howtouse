namespace Alpha.HowToUse
{
    using System.Web.UI;
    using Sunplus.Agent.Model.Entities;

    public partial class Top_Page : Fanex.Age.UI.TplPage
    {    
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
            Top_Template tpl = new Top_Template();
            if (CurrentUserSession.Language.Equals("zh-CN", System.StringComparison.OrdinalIgnoreCase)
                || CurrentUserSession.Language.Equals("zh-TW", System.StringComparison.OrdinalIgnoreCase))
            {
                tpl.idLogo = string.Format("logo_{0}", UserSession.Current.Language);
            }
            else
            {
                tpl.idLogo = "logo_en-US";
            }

            writer.Write(tpl);
            Response.Write(UserSession.Current.Site);
        }
    }
}