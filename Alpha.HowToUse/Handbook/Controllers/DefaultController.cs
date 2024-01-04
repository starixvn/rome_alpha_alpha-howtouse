using System.Web.Mvc;
using Alpha.HowToUse.Handbook.Handlers;
using Alpha.HowToUse.Handbook.ViewModels;
using AlphaPlus.User.Models;

namespace Alpha.HowToUse.Handbook.Controllers
{
    public class DefaultController : Controller
    {
        public CategoryHandler categoryHandler;
        private readonly IUserSession userSession;

        public DefaultController(IUserSession userSession)
        {
            categoryHandler = new CategoryHandler();
            this.userSession = userSession;
        }

        public ActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                Language = userSession.LangKey.ToLowerInvariant(),
                Categories = categoryHandler.GetAll()
            };

            return View("~/Handbook/Views/Default/Index.cshtml", viewModel);
        }
    }
}