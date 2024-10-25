using System.Linq;
using System.Web.Mvc;
using Alpha.HowToUse.Handbook.Consts;
using Alpha.HowToUse.Handbook.Handlers;
using Alpha.HowToUse.Handbook.Models;
using Alpha.HowToUse.Handbook.Services;
using Alpha.HowToUse.Handbook.ViewModels;
using AlphaPlus.User.Models;

namespace Alpha.HowToUse.Handbook.Controllers
{
    public class DefaultController : HandbookBaseController
    {
        private readonly IUserSession userSession;
        public readonly IProfileService profileService;
        public readonly CategoryHandler categoryHandler;

        public DefaultController(
            IUserSession userSession,
            IProfileService profileService)
        {
            this.userSession = userSession;
            this.profileService = profileService;
            categoryHandler = new CategoryHandler(userSession);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Index()
        {
            var profilePicture = profileService.GetProfilePicture(userSession.CustId);
            var briefProfile = new BriefProfileViewModel
            {
                IsDefaultAvatar = string.IsNullOrEmpty(profilePicture.ImageBase64String),
                EmbedImage = profilePicture.ImageBase64String
            };

            var langKey = userSession.LangKey.ToLowerInvariant();
            var locale = LanguageConsts.Locales[langKey];
            var categories = categoryHandler.GetAll() ?? Enumerable.Empty<CategoryModel>();
            foreach (var c in categories)
            {
                if (c.Articles != null)
                {
                    c.Articles = c.Articles.OrderBy(a => a.Order);
                }
            }

            var viewModel = new IndexViewModel
            {
                LanguageKey = langKey,

                OnUserId = userSession.OnUserId,
                BriefProfile = briefProfile,
                Categories = categories.OrderBy(c => c.Order),

                UserId = userSession.UserId,
                CustId = userSession.CustId,
                SubAccId = userSession.SubAccId
            };

            return View("~/Handbook/Views/Default/Index.cshtml", viewModel);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult GetBySlug(string slug)
        {
            var profilePicture = profileService.GetProfilePicture(userSession.CustId);
            var briefProfile = new BriefProfileViewModel
            {
                IsDefaultAvatar = string.IsNullOrEmpty(profilePicture.ImageBase64String),
                EmbedImage = profilePicture.ImageBase64String
            };

            var langKey = userSession.LangKey.ToLowerInvariant();
            var locale = LanguageConsts.Locales[langKey];
            var categories = categoryHandler.GetAll() ?? Enumerable.Empty<CategoryModel>();
            foreach (var c in categories)
            {
                if (c.Articles != null)
                {
                    c.Articles = c.Articles.OrderBy(a => a.Order);
                }
            }

            var viewModel = new GetBySlugViewModel
            {
                LanguageKey = langKey,
                OnUserId = userSession.OnUserId,
                BriefProfile = briefProfile,
                Categories = categories.OrderBy(c => c.Order),
                Slug = slug,

                UserId = userSession.UserId,
                CustId = userSession.CustId,
                SubAccId = userSession.SubAccId
            };

            return View("~/Handbook/Views/Default/GetBySlug.cshtml", viewModel);
        }
    }
}