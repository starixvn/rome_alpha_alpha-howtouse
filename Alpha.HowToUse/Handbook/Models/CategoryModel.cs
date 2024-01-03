using System.Collections.Generic;

namespace Alpha.HowToUse.Handbook.Models
{
    public class CategoryModel
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string ShortName { get; set; }
        public CategoryModel CategoryParent { get; set; }
        public IEnumerable<CategoryModel> Categories { get; set; }
        public IEnumerable<MarketModel> Markets { get; set; }
    }
}