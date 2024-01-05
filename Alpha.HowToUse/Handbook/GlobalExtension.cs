using System.Reflection;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using AlphaPlus.User.Models;
using Fanex.Data;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using Fanex.Data.SqlServer;
using Alpha.HowToUse.Handbook.Configurations;
using Alpha.HowToUse.Handbook.Services;
using Fanex.Data.Repository;
using AlphaPlus.Logging;
using AlphaPlus.Logging.Web;
using Fanex.Logging.Extensions.Database;
using Alpha.Web.Helpers;

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

            AlphaPlus.Configurations.ConfigurationManager.Init(new ConfigurationHelper());
            LoggingConfig.ExceptionHandler = new ExceptionHandlerAdapter();
            var sensitivteInfo = new[] { "password", "seccode", "securitycode" };
            LoggingConfig.Init(sensitivteInfo, DatabaseInfoGetters.GetDbExceptionInfo);

            RegisterDIs();

            DbSettingProviderManager
                .StartNewSession()
                .WithSqlServer(resourcePath: Alpha.HowToUse.Handbook.Configurations.AppSettingsHelper.StoreProcedurePath)
                .Run();
        }

        private static void RegisterDIs()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            UserSession.Init(new Alpha.Web.Helpers.HttpContextHelper());
            container.RegisterPerWebRequest<IUserSession, UserSessionExt>();
            container.RegisterPerWebRequest<IDynamicRepository, DynamicRepository>();
            container.RegisterPerWebRequest<IProfileService, ProfileService>();

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}