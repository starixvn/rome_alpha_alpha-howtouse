using System.Reflection;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using Alpha.Web.Helpers;
using AlphaPlus.User.Models;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;

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

            RegisterDIs();
        }

        private static void RegisterDIs()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            UserSession.Init(new HttpContextHelper());
            container.RegisterPerWebRequest<IUserSession, UserSessionExt>();

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}