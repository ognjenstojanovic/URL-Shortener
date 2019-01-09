using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using UrlShortener.DTO;
using UrlShortener.EntityFramework;
using UrlShortener.Helpers;
using UrlShortener.Models;

namespace UrlShortener.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShortenController : ControllerBase
    {
        private readonly string baseUrl = "http://localhost:62735";
        private readonly UrlShortenerDbContext dbContext;

        public ShortenController(UrlShortenerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult Post([FromBody]ShortenUrlDTO shortenUrlDTO)
        {
            var shortUrl = CreateNewShortUrl(shortenUrlDTO);
            SaveToDatabase(shortUrl);
            return Ok(new ShortenedUrlDTO { Original_link = shortUrl.OriginalUrl, Short_link = shortUrl.Alias });
        }

        private ShortUrl CreateNewShortUrl(ShortenUrlDTO shortenUrlDTO)
        {
            var shortUrl = new ShortUrl { Id = Guid.NewGuid(), OriginalUrl = shortenUrlDTO.Url };
            shortUrl.Alias = baseUrl + RandomStringHelper.RandomString(8);

            while(dbContext.ShortUrls.Any(su => su.Alias == shortUrl.Alias))
            {
                shortUrl.Alias = baseUrl + RandomStringHelper.RandomString(8);
            }

            return shortUrl;
        }

        private void SaveToDatabase(ShortUrl shortUrl)
        {
            dbContext.ShortUrls.Add(shortUrl);
            dbContext.SaveChanges();
        }
    }
}