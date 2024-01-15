using System;
using System.Collections.Generic;
using System.Text;
using Alpha.Web.Helpers;

namespace Alpha.HowToUse.Handbook.Models
{
    public class ArticleModel
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public IEnumerable<CategoryModel> Categories { get; set; }

        public DateTime? PublishedAt { get; set; }
        public string PublishedAtStr => PublishedAt?.ToString("ddd, MMM dd, yyyy") ?? string.Empty;

        public void CorrectImageUrls()
        {
            if (string.IsNullOrWhiteSpace(Content))
            {
                return;
            }

            var imgRegex = "<img ";
            var srcRegex = "src=\"";
            var apiRegex = "/api/images/get/";
            var result = new StringBuilder();

            while (Content.Length > 0)
            {
                var ii = Content.IndexOf(imgRegex);
                if (ii == -1)
                {
                    result.Append(Content);
                    break;
                }

                var p1 = Content.Substring(0, ii);
                Content = Content.Substring(ii);
                result.Append(p1);

                var iSrc = Content.IndexOf(srcRegex);
                if (iSrc == -1)
                {
                    result.Append(Content);
                    break;
                }

                var p2 = Content.Substring(0, iSrc + srcRegex.Length);
                Content = Content.Substring(iSrc + srcRegex.Length);
                result.Append(p2);

                var iApi = Content.IndexOf(apiRegex);
                if (iApi == -1)
                {
                    result.Append(Content);
                    break;
                }

                Content = Content.Substring(iApi + apiRegex.Length);
                result.Append(MicroSites.HowToUseUrl("~/Image/Get?name="));
            }

            Content = result.ToString();
        }
    }
}