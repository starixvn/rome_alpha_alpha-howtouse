namespace Alpha.UserGuide
{
    using System.Web;
    using Auth.Web;
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            ShareSession.Init();
        }
    }
}