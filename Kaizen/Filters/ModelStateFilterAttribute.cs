using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Kaizen.Filters
{
    public class ModelStateFilterAttribute : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Ignored because it's not necessary
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
