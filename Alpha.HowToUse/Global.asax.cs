namespace Alpha.UserGuide
{
    using System;
    using System.Web;
    using System.Web.Security;
    using Alpha.Web.Helpers;
    using Sunplus.Agent.Model.Entities;
    using Web;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            ShareSession.Init();
        }

        void Application_AcquireRequestState(object sender, EventArgs e)
        {
            if (UserSession.Current.IsProtectedMode)
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