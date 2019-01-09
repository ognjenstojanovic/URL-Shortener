using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace UrlShortener.Helpers
{
    public class InternalServerErrorActionResult : IActionResult
    {
        public Task ExecuteResultAsync(ActionContext context)
        {
            var result = new ObjectResult("An unexpected error occured, please try again later.")
            {
                StatusCode = 500
            };

            return result.ExecuteResultAsync(context);
        }
    }
}
