using Microsoft.AspNetCore.Mvc;
using System.Linq;
using UrlShortener.EntityFramework;
using UrlShortener.Models;

namespace UrlShortener.Controllers
{
    [Route("")]
    [ApiController]
    public class RedirectController : ControllerBase
    {
        private readonly UrlShortenerDbContext dbContext;

        public RedirectController(UrlShortenerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("{alias}")]
        public ActionResult<string> Get(string alias)
        {
            var shortUrl = dbContext.ShortUrls.FirstOrDefault(url => url.Alias == alias);

            if (shortUrl != null)
            {
                return Redirect(shortUrl.OriginalUrl);
            }
            else
            {
                return BadRequest("Url does not exist!");
            }
        }
    }
}
