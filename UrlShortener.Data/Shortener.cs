using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Data
{
    public class Shortener
    {
        public string OriginalUrl { get; set; }

        public string ShortUrl { get; set; }


        public Shortener(string url, bool originalUrl = true)
        {
            if (originalUrl)
                OriginalUrl = url;
            else
                ShortUrl = url;
        }

        public void Shorten()
        {
            var existingUrl = ModelDataContext.GetUrl(OriginalUrl);
            if (existingUrl == null)
            {
                var result = ModelDataContext.InsertUrl(new Data.ShortUrl() { OriginalUrl = OriginalUrl });

                ShortUrl = result.ShortenedUrl;
            }
            else
                ShortUrl = existingUrl.ShortenedUrl;
        }

        public void DecodeUrl()
        {
            var shortUrl = ModelDataContext.GetFromEncoded(ShortUrl);
            if (shortUrl != null)
            {
                OriginalUrl = shortUrl.OriginalUrl;
            }
        }

    }
}
