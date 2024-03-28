using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Alpha.HowToUse.Handbook.Handlers;
using Alpha.HowToUse.Handbook.Models;
using Alpha.HowToUse.Handbook.ViewModels;
using Alpha.Web;
using AlphaPlus.User.Models;

namespace Alpha.HowToUse.Handbook.Controllers
{
    public class ArticleController : BaseController
    {
        public ArticleHandler articleHandler;
        private readonly IUserSession userSession;
        private const int SuggestedLimit = 10;

        public ArticleController(IUserSession userSession)
        {
            this.userSession = userSession;
            articleHandler = new ArticleHandler(userSession);
        }

        [HttpGet, ValidateInput(false)]
        public ActionResult Search(string keyword)
        {
            var langKey = userSession.LangKey.ToLowerInvariant();
            var article = articleHandler.Search(keyword) ?? Enumerable.Empty<ArticleModel>();

            var viewModel = new SearchArticleViewModel
            {
                Language = langKey,
                Articles = article.OrderBy(a => a.Title)
            };

            return View("~/Handbook/Views/Article/Search.cshtml", viewModel);
        }

        public ActionResult GetBySlug(string slug)
        {
            var article = articleHandler.GetBySlug(slug);
            var viewModel = new ArticleViewModel
            {
                Article = article,
                Language = userSession.Language
            };

            return View("~/Handbook/Views/Article/GetBySlug.cshtml", viewModel);
        }

        [HttpGet, ValidateInput(false)]
        public JsonResult Suggest(string keyword)
        {
            var langKey = userSession.LangKey.ToLowerInvariant();
            var result = articleHandler.Suggest(keyword)?.Take(SuggestedLimit) ?? new List<SuggestionModel>();
            return Json(result.OrderBy(r => r.Value), JsonRequestBehavior.AllowGet);
        }
    }
}