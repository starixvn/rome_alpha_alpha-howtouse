using System.Collections.Generic;
using Alpha.HowToUse.Handbook.Models;

namespace Alpha.HowToUse.Handbook.ViewModels
{
    public class GlossaryViewModel
    {
        public string Group { get; set; }
        public IEnumerable<GlossaryModel> Glossaries { get; set; }
    }
}