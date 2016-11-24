namespace Alpha.UserGuide
{
    using System.Web;
    using Web;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            ShareSession.Init();
        }
    }
}