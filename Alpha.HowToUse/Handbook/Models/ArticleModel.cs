using System;
using System.Collections.Generic;

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
        public string PublishedAtStr => PublishedAt?.ToString("ddd, MMM dd, yyyyy") ?? string.Empty;
    }
}