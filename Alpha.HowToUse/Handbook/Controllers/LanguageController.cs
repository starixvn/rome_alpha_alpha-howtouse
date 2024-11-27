using Alpha.HowToUse.Handbook.Handlers;
using Alpha.NetThrottling.Mvc;
using AlphaPlus.User.Models;

namespace Alpha.HowToUse.Handbook.Controllers
{
    [DisableNetThrottling]
    public class LanguageController : HandbookBaseController
    {
        private readonly IUserSession userSession;
        public LanguageHandler handler;

        public LanguageController(IUserSession userSession)
        {
            this.userSession = userSession;
            handler = new LanguageHandler(userSession);
        }

        public void Change(string selected)
        {
            handler.Change(selected);
        }
    }
}