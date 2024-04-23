using System;
using System.Web.Mvc;
using System.Web.UI;
using Alpha.HowToUse.Handbook.Handlers;

namespace Alpha.HowToUse.Handbook.Controllers
{
    public class ImageController : HandbookBaseController
    {
        public ImageHandler handler;

        public ImageController()
        {
            handler = new ImageHandler();
        }

        [OutputCache(Duration = 60 * 60 * 24, VaryByParam = "*", Location = OutputCacheLocation.Client)]
        public ActionResult Get(string name)
        {
            var result = handler.Get(name);
            if (string.IsNullOrWhiteSpace(result))
            {
                return null;
            }

            var r = "base64,";
            var start = result.IndexOf(r);

            byte[] imageBytes = Convert.FromBase64String(result.Substring(start + r.Length));
            return File(imageBytes, "image/png", name);
        }
    }
}