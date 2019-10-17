using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Clinicia.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Clinicia.Infrastructure.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other scoped dependencies */)
        {
            try
            {
                // must be awaited
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // if it's not one of the expected exception, set it to 500
            var code = HttpStatusCode.InternalServerError;

            //TODO:Mapping if (exception is NotFoundExe) code = HttpStatusCode.NotFound;
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
                case BusinessException _:
                    code = HttpStatusCode.Unauthorized;
                    break;
            }

            return WriteExceptionAsync(context, exception, code);
        }

        private static Task WriteExceptionAsync(HttpContext context, Exception exception, HttpStatusCode code)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)code;

            return response.WriteAsync(
                JsonConvert.SerializeObject(
                    new
                    {
                        success = false,
                        errorCode = code.ToString(),
                        errorMessage = exception.InnerException != null ? exception.InnerException.Message : exception.Message,
                    }
                ));
        }
    }
}