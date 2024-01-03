using System.Web.Mvc;
using Alpha.HowToUse.Handbook.Handlers;
using Alpha.HowToUse.Handbook.ViewModels;

namespace Alpha.HowToUse.Handbook.Controllers
{
    public class ArticleController : Controller
    {
        public ArticleHandler articleHandler;

        public ArticleController()
        {
            articleHandler = new ArticleHandler();
        }

        public ActionResult Search(string keyword)
        {
            var viewModel = new SearchArticleViewModel
            {
                Articles = articleHandler.Search(keyword)
            };

            return View("~/Handbook/Views/Article/Search.cshtml", viewModel);
        }

        public ActionResult GetBySlug(string slug)
        {
            var viewModel = articleHandler.GetBySlug(slug);
            return View("~/Handbook/Views/Article/GetBySlug.cshtml", viewModel);
        }
    }
}