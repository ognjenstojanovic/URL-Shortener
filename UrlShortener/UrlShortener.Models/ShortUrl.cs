using System;
using System.Collections.Generic;
using System.Text;

namespace UrlShortener.Models
{
    public class ShortUrl
    {
        public Guid Id { get; set; }

        public string Alias { get; set; }

        public string OriginalUrl { get; set; }
    }
}
