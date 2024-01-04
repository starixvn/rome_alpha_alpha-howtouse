using System.Collections.Generic;
using Alpha.HowToUse.Handbook.Models;

namespace Alpha.HowToUse.Handbook.ViewModels
{
    public class IndexViewModel
    {
        public string Language { get; set; }

        public IEnumerable<CategoryModel> Categories { get; set; }
    }
}