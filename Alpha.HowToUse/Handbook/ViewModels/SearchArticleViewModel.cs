using System.Collections.Generic;
using Alpha.HowToUse.Handbook.Models;

namespace Alpha.HowToUse.Handbook.ViewModels
{
    public class SearchArticleViewModel
    {
        public IEnumerable<ArticleModel> Articles { get; set; }
    }
}