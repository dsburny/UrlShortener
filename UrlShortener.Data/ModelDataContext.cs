using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;

namespace UrlShortener.Data
{
    public class ModelDataContext : DbContext
    {
        // Your context has been configured to use a 'Model' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'UrlShortener.Data.Model' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Model' 
        // connection string in the application configuration file.
        public ModelDataContext()
            : base("name=ModelDataContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<ShortUrl> ShortUrls { get; set; }


        public static ShortUrl GetUrl(string url)
        {
            using (var db = new ModelDataContext())
            {
                return db.ShortUrls.FirstOrDefault(x => x.OriginalUrl == url);
            }
        }

        public static ShortUrl GetFromEncoded(string encoded)
        {
            using (var db = new ModelDataContext())
            {
                return db.ShortUrls.FirstOrDefault(x => x.ShortenedUrl == encoded);
            }
        }
        public static ShortUrl InsertUrl(ShortUrl url)
        {
            using (var db = new ModelDataContext())
            {
                db.ShortUrls.Add(url);
                db.SaveChanges();

                var id = url.Id;
                var shortened = Convert.ToBase64String(BitConverter.GetBytes(id));
                url.ShortenedUrl = shortened;

                var savedUrl = db.ShortUrls.FirstOrDefault(x => x.Id == id);
                if(savedUrl != null)
                {
                    savedUrl.ShortenedUrl = shortened;
                    db.SaveChanges();
                }


                return url;


            }
        }
    }

    public class ShortUrl
    {
        [Key]
        public int Id { get; set; }
        public string OriginalUrl { get; set; }

        public string ShortenedUrl { get; set; }
    }
}