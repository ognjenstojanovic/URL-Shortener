using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShortenController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            // Insert record to database
            return Ok();
        }
    }
}