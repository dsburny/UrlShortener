using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web.Mvc;

namespace Shortener.Tests
{
    [TestClass]
    public class HomeControllerTests
    {

        [TestMethod]
        public void ShortenView_Load()
        {
            var controller = new UrlShortener.Controllers.HomeController();
            var result = controller.ShortenUrl() as ViewResult;
            Assert.AreEqual("ShortenUrl", result.ViewName);
        }

        [TestMethod]
        
        public void Decode_Invalid_Url()
        {
            
            var controller = new UrlShortener.Controllers.HomeController();
            var result = controller.DecodeUrl("abc") as ContentResult;
            Assert.AreEqual("URL could not be translated!", result.Content);
        }
    }
}
