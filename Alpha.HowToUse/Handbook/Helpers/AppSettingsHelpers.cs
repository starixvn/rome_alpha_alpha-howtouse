using System.Configuration;

namespace Alpha.HowToUse.Handbook.Configurations
{
    public static class AppSettingsHelpers
    {
        public static string ContentAPIUrl => ConfigurationManager.AppSettings["ContentAPIUrl"];
    }
}