using System.Web.Mvc;
using Alpha.HowToUse.Handbook.Consts;
using Alpha.HowToUse.Handbook.Handlers;
using Alpha.HowToUse.Handbook.ViewModels;
using Alpha.Web;
using AlphaPlus.User.Models;

namespace Alpha.HowToUse.Handbook.Controllers
{
    public class CategoryController : BaseController
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
            var viewModel = new CategoryViewModel
            {
                Language = locale,
                Category = handler.GetBySlug(slug)
            };

            return View("~/Handbook/Views/Category/GetBySlug.cshtml", viewModel);
        }
    }
}