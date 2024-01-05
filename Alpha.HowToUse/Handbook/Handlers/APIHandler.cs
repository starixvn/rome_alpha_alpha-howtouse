using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Alpha.HowToUse.Handbook.Handlers
{
    public class APIHandler
    {
        public T Get<T>(string url)
        {
            var content = Get(url);

            return DeserializeObject<T>(content);
        }

        private string Get(string url)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                //  client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AppSettingsHelper.ContentAPIToken);
                var response = client.GetAsync(url).GetAwaiter().GetResult();
                var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                return content;
            }
        }

        private T DeserializeObject<T>(string content)
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var result = JsonConvert.DeserializeObject<T>(content, settings);

            return result;
        }
    }
}