using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.Controllers
{
    [Route("")]
    [ApiController]
    public class RedirectController : ControllerBase
    {
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            // retreive record from database
            return Redirect("http://www.google.com");
        }
    }
}
