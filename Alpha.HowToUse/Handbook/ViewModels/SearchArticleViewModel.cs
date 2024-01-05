using System.Collections.Generic;
using Alpha.HowToUse.Handbook.Consts;
using Alpha.HowToUse.Handbook.Models;

namespace Alpha.HowToUse.Handbook.ViewModels
{
    public class SearchArticleViewModel
    {
        public IEnumerable<ArticleModel> Articles { get; set; }
        public string Language { get; set; }
        public string Locale => LanguageConsts.Locales[Language];
    }
}