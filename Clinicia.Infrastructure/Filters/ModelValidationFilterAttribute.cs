using System.Linq;
using System.Threading.Tasks;
using Clinicia.Common.Enums;
using Clinicia.Common.Extensions;
using Clinicia.Infrastructure.ApiResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Clinicia.Infrastructure.Filters
{
    public class ModelValidationFilterAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var error = context.ModelState
                    .FirstOrDefault(x => x.Value.Errors.Any())
                    .Value
                    .Errors.FirstOrDefault();

                context.Result = new BadRequestObjectResult(
                    new ApiErrorResult
                    {
                        Success = false,
                        ErrorCode = ErrorCodes.InvalidParameters.ToString(),
                        ErrorMessage = error?.ErrorMessage != null && error.ErrorMessage.IsNullOrEmpty() ? error.Exception.Message : error?.ErrorMessage,
                    });

                return;
            }

            await next();
        }
    }
}