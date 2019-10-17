using Clinicia.Common.Exceptions;
using Clinicia.Common.Extensions;
using Clinicia.Infrastructure.ApiResults;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;

namespace Clinicia.Infrastructure.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _env;
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        public HttpGlobalExceptionFilter(IHostingEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
        {
            _env = env;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            _logger.LogError(
                new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);

            if (context.Exception.GetType() == typeof(BusinessException) || context.Exception.GetType().BaseType == typeof(BusinessException))
            {
                // handle bussiness exception
                var json = new ApiErrorResult
                {
                    Success = false,
                    ErrorCode = (context.Exception as BusinessException)?.ErrorCode,
                    ErrorMessage = context.Exception.Message,
                };

                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                // if it's not one of the expected exception, set it to 500
                var code = HttpStatusCode.InternalServerError;

                switch (exception)
                {
                    case EntityNotFoundException _:
                        code = HttpStatusCode.NotFound;
                        break;
                    case ArgumentNullException _:
                        code = HttpStatusCode.BadRequest;
                        break;
                    case InvalidArgumentException _:
                        code = HttpStatusCode.BadRequest;
                        break;
                    case HttpRequestException _:
                        code = HttpStatusCode.BadRequest;
                        break;
                    case UnauthorizedAccessException _:
                        code = HttpStatusCode.Unauthorized;
                        break;
                }

                context.Result = new ObjectResult(
                    new ApiErrorResult
                    {
                        Success = false,
                        ErrorCode = code.ToString(),
                        ErrorMessage = exception.InnerException != null ? exception.InnerException.Message : exception.Message,
                        TechnicalLog = exception.GetExceptionTechnicalInfo(),
                    });
                context.HttpContext.Response.StatusCode = (int)code;
            }
            context.ExceptionHandled = true;
        }
    }
}