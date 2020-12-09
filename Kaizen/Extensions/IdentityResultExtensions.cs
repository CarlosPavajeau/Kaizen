using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Extensions
{
    public static class IdentityResultExtensions
    {
        public static ActionResult IdentityResultErrors(this ControllerBase controller, IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                controller.ModelState.AddModelError(error.Code, error.Description);
            }

            return controller.BadRequest(new ValidationProblemDetails(controller.ModelState)
            {
                Status = StatusCodes.Status400BadRequest
            });
        }
    }
}
