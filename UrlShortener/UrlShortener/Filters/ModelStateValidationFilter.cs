using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UrlShortener.DTO;

namespace UrlShortener.Filters
{
    public class ModelStateValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }

            if(context.ActionArguments.TryGetValue("shortenUrlDTO", out var argument))
            {

                if (!(argument is ShortenUrlDTO shortenUrlDTO))
                {
                    context.Result = new BadRequestObjectResult(context.ModelState);
                }
                else
                {
                    if (string.IsNullOrEmpty(shortenUrlDTO.Url))
                    {
                        context.Result = new BadRequestObjectResult(context.ModelState);
                    }
                }
            }
            else
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
