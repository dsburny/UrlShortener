using UrlShortener.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace UrlShortener.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShortenUrl()
        {
            return View("ShortenUrl");
        }

        [HttpPost]
        public ActionResult ShortenUrl(ShortenUrlViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                if ( !string.IsNullOrWhiteSpace(viewmodel.Url))
                {
                    if (Uri.TryCreate(viewmodel.Url, UriKind.Absolute, out Uri result))
                    {
                        var shortener = new UrlShortener.Data.Shortener(result.ToString());
                        shortener.Shorten();

                        return new JsonResult() { Data = string.Format("{0}://{1}:{2}/Short/{3}", Request.Url.Scheme, Request.Url.Host, Request.Url.Port, shortener.ShortUrl) };
                    }
                    else
                    {
                        //error
                        ModelState.AddModelError("InvalidUrl", new Exception("The url provided is invalid.Please try again with a valid url"));

                    }
                }
            }
            return View(viewmodel);
        }

        public ActionResult DecodeUrl(string shortUrl)
        {
            if (!string.IsNullOrWhiteSpace(shortUrl))
            {
                    var shortener = new UrlShortener.Data.Shortener(shortUrl, false);
                shortener.DecodeUrl();
                    if(shortener != null && shortener.OriginalUrl != null)
                    {
                        return Redirect(shortener.OriginalUrl);
                    }
                

            }
            return new ContentResult() { Content = "URL could not be translated!" };
        }
    }
}
