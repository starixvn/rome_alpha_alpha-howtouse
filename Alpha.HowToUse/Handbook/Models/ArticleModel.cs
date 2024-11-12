using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Alpha.HowToUse.Handbook.Consts;
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
        public string ReferenceURL { get; set; }
        public int? Order { get; set; }
        public IEnumerable<CategoryModel> Categories { get; set; }

        public DateTime? PublishedAt { get; set; }
        public string PublishedAtStr => PublishedAt?.ToString("ddd, MMM dd, yyyy") ?? string.Empty;

        public string TargetFrame => GetTargetFrame();
        public string Url => GetUrl();

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

        public string GetTargetFrame()
            => ReferenceURLConsts.IsReference(ReferenceURL) ? "_blank" : "main";

        public string GetUrl(string relativePath = "")
        {
            if (string.IsNullOrWhiteSpace(ReferenceURL))
            {
                return AppPathHelper.GetRelativePath("site-howtouse", $"{relativePath}Article/GetBySlug?slug=" + Slug);
            }

            if (ReferenceURL.StartsWith("http"))
            {
                return ReferenceURL;
            }

            var cookieless = HttpContext.Current.Session.IsCookieless ? $"/(S({HttpContext.Current.Session.SessionID}))" : string.Empty;
            switch (ReferenceURL)
            {
                case ReferenceURLConsts.CustomerSettingInformation:
                    return "/site-memberinfo" + cookieless + "/SettingHandbook/SportBookInformation?tabindex=0";
                default:
                    return AppPathHelper.GetRelativePath("site-howtouse", $"{relativePath}Article/GetBySlug?slug=" + Slug);
            }
        }
    }
}