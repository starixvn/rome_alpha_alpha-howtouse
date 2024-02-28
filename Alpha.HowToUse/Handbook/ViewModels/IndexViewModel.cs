using System.Collections.Generic;
using Alpha.HowToUse.Handbook.Consts;
using Alpha.HowToUse.Handbook.Models;
using Alpha.Web.Helpers;

namespace Alpha.HowToUse.Handbook.ViewModels
{
    public class IndexViewModel
    {
        public string Language { get; set; }
        public int OnUserId { get; set; }

        public bool SupportVietnamese => Configurations.AppSettingsHelper.SupportVietnamese;
        public string LanguageName => LanguageConsts.LanguageNames[Language];
        public string Locale => LanguageConsts.Locales[Language];
        public string OldMainRootPath => MicroSites.OldMainUrl("~/");

        public BriefProfileViewModel BriefProfile { get; set; }

        public IEnumerable<CategoryModel> Categories { get; set; }

        public int CustId { get; set; }
        public int UserId { get; set; }
    }
}