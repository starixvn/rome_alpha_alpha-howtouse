using System.Collections.Generic;
using Alpha.HowToUse.Handbook.Consts;
using Alpha.HowToUse.Handbook.Models;
using Alpha.Web.Helpers;

namespace Alpha.HowToUse.Handbook.ViewModels
{
    public class IndexViewModel
    {
        public string LanguageKey { get; set; }
        public int OnUserId { get; set; }

        public bool SupportVietnamese => Configurations.AppSettingsHelper.SupportVietnamese;
        public string LanguageName => LanguageConsts.LanguageNames[LanguageKey];
        public string Locale => LanguageConsts.Locales[LanguageKey];
        public string LanguageCode => LanguageConsts.Codes[LanguageKey];
        public string OldMainRootPath => MicroSites.OldMainUrl("~/");
        public string MessageRootPath => MicroSites.MessageUrl("~/");

        public BriefProfileViewModel BriefProfile { get; set; }

        public IEnumerable<CategoryModel> Categories { get; set; }

        public int CustId { get; set; }
        public int SubAccId { get; set; }
        public int UserId { get; set; }
    }
}