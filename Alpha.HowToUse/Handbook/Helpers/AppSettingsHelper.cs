using System.Configuration;

namespace Alpha.HowToUse.Handbook.Configurations
{
    public static class AppSettingsHelper
    {
        public static string ContentAPIEnvironment => ConfigurationManager.AppSettings["ContentAPI:Environment"];
        public static string ContentAPIUrl => ConfigurationManager.AppSettings["ContentAPIUrl"];
        public static string ContentAPIToken => ConfigurationManager.AppSettings["ContentAPIToken"];

        public static string StoreProcedurePath => ConfigurationManager.AppSettings["StoredProcedurePath"];

        public static string SiteLabel => ConfigurationManager.AppSettings["SiteLabel"];
        public static string SiteLabel789y => ConfigurationManager.AppSettings["SiteLabel789y"];
        public static bool IsSiteLabel789y => SiteLabel.Equals(SiteLabel789y);
        public static bool SupportVietnamese => IsSiteLabel789y;
    }
}