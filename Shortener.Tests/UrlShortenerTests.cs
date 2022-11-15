using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Shortener.Tests
{
    [TestClass]
    public class UrlShortenerTests
    {
        [TestMethod]
        public void TestShortener()
        {
            var shortener = new UrlShortener.Data.Shortener("https://www.google.com");
            shortener.Shorten();

            Assert.IsTrue(!string.IsNullOrWhiteSpace(shortener.ShortUrl));
        }
    }
}
