namespace Alpha.HowToUse
{
    using System.Web.UI;
    using Sunplus.Agent.Model.BL.Definitions;
    using Sunplus.Agent.Model.Entities;

    public partial class Content_Page : Fanex.Age.UI.TplPage
    {
        // Folder Name
        private const string CasinoGuide = "CasinoGuide";
        private const string AccInfo = "AccInfo";
        private const string AutoPT = "AutoPT";
        private const string ViewLog = "ViewLog";
        private const string NumberGame = "NumberGame";
        private const string AgentWapUI = "WapUI";

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
            string parameter = string.Empty, path = string.Empty;
            parameter = string.Format("{0}&{1}&{2}", folderName, CurrentUserSession.RoleId, UserSession.Current.Language);
            parameter = Sunplus.Agent.Utilities.Encryption.CfsEnCrypt(parameter);
            tpl.source = string.Format("{0}Handlers/Flash.ashx?params={1}", AppSettings.BaseUrl, parameter);

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
                case CasinoGuide:
                    title = LocalResources.HowToUse.HowToEnableCasino;
                    break;
                case AccInfo:
                    title = LocalResources.HowToUse.AccInfoFunction;
                    break;
                case ViewLog:
                    title = LocalResources.HowToUse.ViewLogFunction;
                    break;
                case NumberGame:
                    title = LocalResources.HowToUse.NumberGame;
                    break;
                case AutoPT:
                    title = CurrentUserSession.IsSuper ? LocalResources.HowToUse.MasterAutoPT : LocalResources.HowToUse.AutoPT;
                    break;
                case AgentWapUI:
                    title = LocalResources.HowToUse.AgentWapUserGuide;
                    break;
                default:
                    break;
            }

            return title;
        }
    }
}