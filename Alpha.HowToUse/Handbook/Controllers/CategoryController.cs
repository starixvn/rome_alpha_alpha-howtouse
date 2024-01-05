using System.Web.Mvc;
using Alpha.HowToUse.Handbook.Handlers;
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
            var viewModel = handler.GetBySlug(slug);
            return View("~/Handbook/Views/Category/GetBySlug.cshtml", viewModel);
        }
    }
}