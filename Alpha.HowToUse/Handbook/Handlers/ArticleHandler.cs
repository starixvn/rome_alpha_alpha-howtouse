using System.Collections.Generic;
using Alpha.HowToUse.Handbook.Configurations;
using Alpha.HowToUse.Handbook.Models;

namespace Alpha.HowToUse.Handbook.Handlers
{
    public class ArticleHandler
    {
        public APIHandler apiHandler;
        private string contentAPIUrl = AppSettingsHelpers.ContentAPIUrl;

        public ArticleHandler()
        {
            apiHandler = new APIHandler();
        }

        public IEnumerable<ArticleModel> Search(string keyword)
        {
            var url = contentAPIUrl + "/api/article/search/" + keyword;
            var result = apiHandler.Get<IEnumerable<ArticleModel>>(url);
            return result;
        }

        public ArticleModel GetBySlug(string slug)
        {
            var url = contentAPIUrl + "/api/article/get/" + slug;
            var result = apiHandler.Get<ArticleModel>(url);
            return result;
        }
    }
}