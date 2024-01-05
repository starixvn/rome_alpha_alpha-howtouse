using System.Collections.Generic;
using Alpha.HowToUse.Handbook.Configurations;
using Alpha.HowToUse.Handbook.Consts;
using Alpha.HowToUse.Handbook.Models;
using AlphaPlus.User.Models;

namespace Alpha.HowToUse.Handbook.Handlers
{
    public class ArticleHandler
    {
        public APIHandler apiHandler;
        private readonly IUserSession userSession;
        private string contentAPIUrl = AppSettingsHelper.ContentAPIUrl;

        public ArticleHandler(IUserSession userSession)
        {
            this.userSession = userSession;
            apiHandler = new APIHandler();
        }

        public IEnumerable<ArticleModel> Search(string keyword)
        {
            var langKey = userSession.LangKey.ToLowerInvariant();
            var url = contentAPIUrl + "/api/articles/search" +
                "/" + LanguageConsts.Locales[langKey] +
                "/" + AppSettingsHelper.SiteLabel +
                "/" + userSession.RoleId +
                "/k" + keyword;
            var result = apiHandler.Get<IEnumerable<ArticleModel>>(url);
            return result;
        }

        public ArticleModel GetBySlug(string slug)
        {
            var langKey = userSession.LangKey.ToLowerInvariant();
            var url = contentAPIUrl + "/api/articles/get/" + slug +
                "/" + LanguageConsts.Locales[langKey] +
                "/" + AppSettingsHelper.SiteLabel +
                "/" + userSession.RoleId;
            var result = apiHandler.Get<ArticleModel>(url);
            return result;
        }

        public IEnumerable<SuggestionModel> Suggest(string keyword)
        {
            var langKey = userSession.LangKey.ToLowerInvariant();
            var url = contentAPIUrl + "/api/articles/suggest" +
                "/" + LanguageConsts.Locales[langKey] +
                "/" + AppSettingsHelper.SiteLabel +
                "/" + userSession.RoleId +
                "/" + keyword;
            var result = apiHandler.Get<IEnumerable<SuggestionModel>>(url);
            return result;
        }
    }
}