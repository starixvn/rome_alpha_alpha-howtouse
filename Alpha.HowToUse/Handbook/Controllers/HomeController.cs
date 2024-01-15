using System.Web.Mvc;
using Alpha.HowToUse.Handbook.ViewModels;
using Alpha.Web;
using AlphaPlus.User.Models;

namespace Alpha.HowToUse.Handbook.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IUserSession userSession;

        public HomeController(IUserSession userSession)
        {
            this.userSession = userSession;
        }

        public ActionResult Index()
        {
            var langKey = userSession.LangKey.ToLowerInvariant();
            var viewModel = new HomeViewModel();
            return View("~/Handbook/Views/Home/Index.cshtml", viewModel);
        }

        public ActionResult Blank()
        {
            return View("~/Handbook/Views/Home/Blank.cshtml");
        }
    }
}