using System.Web.Mvc;
using Alpha.HowToUse.Handbook.Handlers;
using Alpha.HowToUse.Handbook.ViewModels;
using Alpha.Web;
using AlphaPlus.User.Models;

namespace Alpha.HowToUse.Handbook.Controllers
{
    public class GlossaryController : BaseController
    {
        private readonly IUserSession userSession;
        public GlossaryHandler glossaryHandler;

        public GlossaryController(IUserSession userSession)
        {
            this.userSession = userSession;
            glossaryHandler = new GlossaryHandler(userSession);
        }

        public ActionResult Index(string group)
        {
            var langKey = userSession.LangKey.ToLowerInvariant();
            var viewModel = new GlossaryViewModel
            {
                Group = group,
                Glossaries = glossaryHandler.Search(group)
            };

            return View("~/Handbook/Views/Glossary/Index.cshtml", viewModel);
        }
    }
}