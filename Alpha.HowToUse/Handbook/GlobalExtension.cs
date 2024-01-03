using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;

namespace Alpha.HowToUse.Handbook
{
    public static class GlobalExtension
    {
        public static void Init()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            MvcHandler.DisableMvcResponseHeader = true;

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AntiForgeryConfig.SuppressXFrameOptionsHeader = true;
        }
    }
}