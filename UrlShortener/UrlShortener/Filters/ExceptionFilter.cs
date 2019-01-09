using Microsoft.AspNetCore.Mvc.Filters;
using UrlShortener.Helpers;

namespace UrlShortener.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new InternalServerErrorActionResult();
        }
    }
}
