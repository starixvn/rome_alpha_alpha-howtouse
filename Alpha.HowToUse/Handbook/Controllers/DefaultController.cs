using System.Web.Mvc;
using Alpha.HowToUse.Handbook.Handlers;
using Alpha.HowToUse.Handbook.ViewModels;

namespace Alpha.HowToUse.Handbook.Controllers
{
    public class DefaultController : Controller
    {
        public CategoryHandler categoryHandler;

        public DefaultController()
        {
            categoryHandler = new CategoryHandler();
        }

        public ActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                Categories = categoryHandler.GetAll()
            };

            return View("~/Handbook/Views/Default/Index.cshtml", viewModel);
        }
    }
}