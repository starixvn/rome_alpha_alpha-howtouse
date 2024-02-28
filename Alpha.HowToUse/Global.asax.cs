namespace Alpha.UserGuide
{
    using Alpha.HowToUse.Handbook;
    using Alpha.HowToUse.Helpers;
    using Alpha.Web.Helpers;
    using AlphaPlus.Configurations;
    using AlphaPlus.User.Models;
    using System;
    using System.Web;
    using System.Web.Security;
    using TplNetCore;
    using Web;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalExtension.Init();

            AlphaPlus.Configurations.ConfigurationManager.Init(new ConfigurationHelper());
            TplSettings.UrlVersion = AppSettings.JsVersion;
            TplSettings.Init(new TplSettingHelper());
            UserSession.Init(new HttpContextHelper());
            ShareSession.Init();
        }

        void Application_AcquireRequestState(object sender, EventArgs e)
        {
            if (UserSession.Current.IsProtectedMode || UserSession.Current.UserId <= 0)
            {
                TerminateSessionAndSignOut();
            }
        }

        private void TerminateSessionAndSignOut()
        {
            Session.Clear();

            if (!Session.IsCookieless)
            {
                FormsAuthentication.SignOut();
            }

            Response.Redirect(MicroSites.OldMainUrl(FormsAuthentication.LoginUrl), true);
        }
    }
}