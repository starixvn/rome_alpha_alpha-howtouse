using System.Web.Mvc;
using Alpha.HowToUse.Handbook.Handlers;
using Alpha.HowToUse.Handbook.ViewModels;

namespace Alpha.HowToUse.Handbook.Controllers
{
    public class GlossaryController : Controller
    {
        public GlossaryHandler glossaryHandler;

        public GlossaryController()
        {
            glossaryHandler = new GlossaryHandler();
        }

        public ActionResult Index()
        {
            var viewModel = new GlossaryViewModel
            {
                Glossaries = glossaryHandler.GetAll()
            };

            return View("~/Handbook/Views/Glossary/Index.cshtml", viewModel);
        }
    }
}