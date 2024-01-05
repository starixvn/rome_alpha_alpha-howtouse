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

        public IEnumerable<ArticleModel> Search(string keyword, string language)
        {
            var url = contentAPIUrl + "/api/articles/search" +
                "/" + LanguageConsts.Locales[language] +
                "/" + AppSettingsHelper.SiteLabel +
                "/" + userSession.RoleId +
                "/k" + keyword;
            var result = apiHandler.Get<IEnumerable<ArticleModel>>(url);
            return result;
        }

        public ArticleModel GetBySlug(string slug, string language)
        {
            var url = contentAPIUrl + "/api/articles/get/" + slug +
                "/" + language +
                "/" + AppSettingsHelper.SiteLabel +
                "/" + userSession.RoleId;
            var result = apiHandler.Get<ArticleModel>(url);
            return result;
        }
    }
}