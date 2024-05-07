using System.Collections.Generic;
using Alpha.HowToUse.Handbook.Configurations;
using Alpha.HowToUse.Handbook.Consts;
using Alpha.HowToUse.Handbook.Models;
using AlphaPlus.User.Models;

namespace Alpha.HowToUse.Handbook.Handlers
{
    public class GlossaryHandler
    {
        public APIHandler apiHandler;
        private readonly IUserSession userSession;
        private string contentAPIUrl = AppSettingsHelper.ContentAPIUrl;

        public GlossaryHandler(IUserSession userSession)
        {
            apiHandler = new APIHandler();
            this.userSession = userSession;
        }

        public IEnumerable<GlossaryModel> GetAll()
        {
            var url = contentAPIUrl + "/api/glossaries/latest/all" +
                "/" + LanguageConsts.Locales[userSession.LangKey.ToLowerInvariant()] +
                "/" + AppSettingsHelper.SiteLabel +
                "/" + userSession.RoleId;

            var result = apiHandler.Get<IEnumerable<GlossaryModel>>(url);
            return result;
        }

        public IEnumerable<GlossaryModel> Search(string group)
        {
            var url = contentAPIUrl + "/api/glossaries/latest/g" + group +
                "/" + LanguageConsts.Locales[userSession.LangKey.ToLowerInvariant()] +
                "/" + AppSettingsHelper.SiteLabel +
                "/" + userSession.RoleId;
            var result = apiHandler.Get<IEnumerable<GlossaryModel>>(url);
            return result;
        }
    }
}