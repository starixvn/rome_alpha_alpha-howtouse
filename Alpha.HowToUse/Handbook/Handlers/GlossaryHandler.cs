using System.Collections.Generic;
using Alpha.HowToUse.Handbook.Configurations;
using Alpha.HowToUse.Handbook.Models;

namespace Alpha.HowToUse.Handbook.Handlers
{
    public class GlossaryHandler
    {
        public APIHandler apiHandler;
        private string contentAPIUrl = AppSettingsHelpers.ContentAPIUrl;

        public GlossaryHandler()
        {
            apiHandler = new APIHandler();
        }

        public IEnumerable<GlossaryModel> GetAll()
        {
            var url = contentAPIUrl + "/api/glossary/latest/all";
            var result = apiHandler.Get<IEnumerable<GlossaryModel>>(url);
            return result;
        }
    }
}