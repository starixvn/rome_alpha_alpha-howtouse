using System.Globalization;
using Alpha.HowToUse.Handbook.Configurations;
using Alpha.HowToUse.Handbook.Consts;
using AlphaPlus.User.Models;

namespace Alpha.HowToUse.Handbook.Handlers
{
    public class LanguageHandler
    {
        private readonly IUserSession userSession;
        private readonly CultureInfo defaultCulture = CultureInfo.CreateSpecificCulture("en-US");

        public LanguageHandler(IUserSession userSession)
        {
            this.userSession = userSession;
        }

        public string Change(string selected)
        {
            var languageKey = Standardize(selected);
            var supportVietnamese = AppSettingsHelper.SupportVietnamese;

            if (!supportVietnamese && languageKey == LanguageConsts.VietnameseKey)
            {
                languageKey = LanguageConsts.EnglishKey;
            }

            var code = LanguageConsts.Codes[languageKey];
            userSession.Language = code;
            userSession.LangKey = languageKey;

            return languageKey;
        }

        private string Standardize(string languageKey)
            => string.IsNullOrWhiteSpace(languageKey) || !LanguageConsts.LanguageNames.ContainsKey(languageKey)
            ? LanguageConsts.Default
            : languageKey;
    }
}