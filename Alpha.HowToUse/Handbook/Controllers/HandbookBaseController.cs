using System.Web.Mvc;
using System.Web.Security;
using Alpha.HowToUse.Handbook.Configurations;
using Alpha.Web;

namespace Alpha.HowToUse.Handbook.Controllers
{
    public class HandbookBaseController : BaseController
    {
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (AppSettingsHelper.SupportedNewHandbook)
            {
                return;
            }

            filterContext.Result = new HttpUnauthorizedResult("You are not authorized on this function!");

            var session = filterContext.HttpContext.Session;
            session.Clear();
            session.Abandon();
            if (!session.IsCookieless)
            {
                FormsAuthentication.SignOut();
            }
        }
    }
}