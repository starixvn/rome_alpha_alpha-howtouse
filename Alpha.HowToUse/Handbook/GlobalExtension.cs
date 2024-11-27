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
using Alpha.NetThrottling.Mvc;
using Alpha.NetThrottling.Utils;
using Alpha.NetThrottling;
using Alpha.NetThrottling.Models;
using Alpha.NetThrottling.Resolvers;

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

            GlobalFilters.Filters.Add(new NetThrottlingFilterAttribute(isUsedRedis: false));

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

            RegisterNetThrottling(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void RegisterNetThrottling(Container container)
        {
            container.Register<INetThrottler>(() => new NetThrottler(false));
            container.Register<IThrottleSetting>(() => new ThrottleSetting(false));
            container.Register<IClock, Clock>(Lifestyle.Scoped);
            container.Register<ILogger, Logger>(Lifestyle.Scoped);
            container.Register<ICacheServiceFactory, CacheServiceFactory>(Lifestyle.Scoped);
            container.Register<IResolver, BaseResolver>(Lifestyle.Scoped);
        }
    }
}