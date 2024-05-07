using Alpha.HowToUse.Handbook.Configurations;

namespace Alpha.HowToUse.Handbook.Handlers
{
    public class ImageHandler
    {
        public APIHandler apiHandler;
        private string contentAPIUrl = AppSettingsHelper.ContentAPIUrl;

        public ImageHandler()
        {
            apiHandler = new APIHandler();
        }

        public string Get(string name)
        {
            var url = contentAPIUrl + "/api/images/base64/" + name;
            var result = apiHandler.Get(url);
            return result;
        }
    }
}