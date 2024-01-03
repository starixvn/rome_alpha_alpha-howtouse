using System.Collections.Generic;
using Alpha.HowToUse.Handbook.Models;

namespace Alpha.HowToUse.Handbook.ViewModels
{
    public class GlossaryViewModel
    {
        public IEnumerable<GlossaryModel> Glossaries { get; set; }
    }
}