using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UrlShortener.Models
{
    public class ShortenUrlViewModel
    {
        [Required]
        [Display(Name = "Url")]        
        [Url]
        public string Url { get; set; }

    }

}