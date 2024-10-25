using System;
using System.Configuration;
using System.Linq;

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

        public static bool SupportedNewHandbook
        {
            get
            {
                var marketsSupportedNewHandbook = ConfigurationManager.AppSettings["MarketsSupportedNewHandbook"];

                if (string.IsNullOrWhiteSpace(marketsSupportedNewHandbook))
                {
                    return false;
                }

                if (string.Equals("*", marketsSupportedNewHandbook, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                var label = SiteLabel;
                return marketsSupportedNewHandbook
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Any(s => string.Equals(s, label, StringComparison.OrdinalIgnoreCase));
            }
        }
    }
}