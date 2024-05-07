using System.Linq;
using System.Web.Mvc;
using Alpha.HowToUse.Handbook.Consts;
using Alpha.HowToUse.Handbook.Handlers;
using Alpha.HowToUse.Handbook.Models;
using Alpha.HowToUse.Handbook.ViewModels;
using AlphaPlus.User.Models;

namespace Alpha.HowToUse.Handbook.Controllers
{
    public class CategoryController : HandbookBaseController
    {
        public CategoryHandler handler;
        private readonly IUserSession userSession;

        public CategoryController(IUserSession userSession)
        {
            this.userSession = userSession;
            handler = new CategoryHandler(userSession);
        }

        public ActionResult GetBySlug(string slug)
        {
            var langKey = userSession.LangKey.ToLowerInvariant();
            var locale = LanguageConsts.Locales[langKey];
            var category = handler.GetBySlug(slug) ?? new CategoryModel();
            category.Articles = category.Articles.OrderBy(a => a.Order);

            var viewModel = new CategoryViewModel
            {
                Language = locale,
                Category = category
            };

            return View("~/Handbook/Views/Category/GetBySlug.cshtml", viewModel);
        }
    }
}