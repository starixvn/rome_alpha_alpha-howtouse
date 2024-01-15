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

        public ArticleController(IUserSession userSession)
        {
            this.userSession = userSession;
            articleHandler = new ArticleHandler(userSession);
        }

        public ActionResult Search(string keyword)
        {
            var langKey = userSession.LangKey.ToLowerInvariant();
            var viewModel = new SearchArticleViewModel
            {
                Language = langKey,
                Articles = articleHandler.Search(keyword) ?? Enumerable.Empty<ArticleModel>()
            };

            return View("~/Handbook/Views/Article/Search.cshtml", viewModel);
        }

        public ActionResult GetBySlug(string slug)
        {
            var viewModel = articleHandler.GetBySlug(slug);
            return View("~/Handbook/Views/Article/GetBySlug.cshtml", viewModel);
        }

        public JsonResult Suggest(string keyword)
        {
            var langKey = userSession.LangKey.ToLowerInvariant();
            var result = articleHandler.Suggest(keyword);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}