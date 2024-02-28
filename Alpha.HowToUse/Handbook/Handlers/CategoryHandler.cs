using System.Collections.Generic;
using Alpha.HowToUse.Handbook.Configurations;
using Alpha.HowToUse.Handbook.Consts;
using Alpha.HowToUse.Handbook.Models;
using AlphaPlus.User.Models;

namespace Alpha.HowToUse.Handbook.Handlers
{
    public class CategoryHandler
    {
        public APIHandler apiHandler;
        private readonly IUserSession userSession;
        private string contentAPIUrl = AppSettingsHelper.ContentAPIUrl;

        public CategoryHandler(IUserSession userSession)
        {
            apiHandler = new APIHandler();
            this.userSession = userSession;
        }

        public IEnumerable<CategoryModel> GetAll()
        {
            var url = contentAPIUrl + "/api/categories/latest/all" +
                "/" + LanguageConsts.Locales[userSession.LangKey.ToLowerInvariant()] +
                "/" + AppSettingsHelper.SiteLabel +
                "/" + userSession.RoleId +
                "/" + AppSettingsHelper.ContentAPIEnvironment;
            var categories = apiHandler.Get<IEnumerable<CategoryModel>>(url);

            return categories;
        }

        public CategoryModel GetBySlug(string slug)
        {
            var url = contentAPIUrl + "/api/categories/latest" +
                "/" + slug +
                "/" + LanguageConsts.Locales[userSession.LangKey.ToLowerInvariant()] +
                "/" + AppSettingsHelper.SiteLabel +
                "/" + userSession.RoleId +
                "/" + AppSettingsHelper.ContentAPIEnvironment;
            var category = apiHandler.Get<CategoryModel>(url);

            return category;
        }
    }
}