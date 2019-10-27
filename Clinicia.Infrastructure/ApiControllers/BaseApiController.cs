using Clinicia.Common.Enums;
using Clinicia.Common.Exceptions;
using Clinicia.Dtos.Common;
using Clinicia.Infrastructure.ApiResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mime;
using System.Security.Authentication;
using System.Security.Claims;

namespace Clinicia.Infrastructure.ApiControllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    public class BaseApiController : Controller
    {
        protected BadRequestObjectResult BadRequest(ErrorCodes code)
        {
            return BadRequest(
                new ApiErrorResult
                {
                    Success = false,
                    ErrorCode = code.ToString(),
                    ErrorMessage = code.ToString(),
                });
        }

        protected BadRequestObjectResult BadRequest(ErrorCodes code, string message)
        {
            return BadRequest(
                new ApiErrorResult
                {
                    Success = false,
                    ErrorCode = code.ToString(),
                    ErrorMessage = message,
                });
        }

        protected BadRequestObjectResult BadRequest(string message)
        {
            return BadRequest(
                new ApiErrorResult
                {
                    Success = false,
                    ErrorCode = ErrorCodes.Failed.ToString(),
                    ErrorMessage = message,
                });
        }

        public override NotFoundObjectResult NotFound(object value)
        {
            return new NotFoundObjectResult(
                new ApiErrorResult
                {
                    Success = false,
                    ErrorCode = ErrorCodes.ObjectNotFound.ToString(),
                    ErrorMessage = $"Object with id = {value} not found",
                });
        }

        protected OkObjectResult Success(object result)
        {
            return new OkObjectResult(
                new ApiSuccessResult
                {
                    Success = true,
                    Result = result,
                });
        }

        protected OkObjectResult Success()
        {
            return new OkObjectResult(
                new ApiResult
                {
                    Success = true,
                });
        }

        protected FileResult AttachmentResult(FileDescription file)
        {
            // We can have record in database, but no actual file in file storage
            if (file.Data == null)
            {
                throw new BusinessException(ErrorCodes.FileNotFound.ToString(), "File doesn't exits");
            }

            var contentDisposition = new ContentDisposition
            {
                FileName = file.FileName,
                Inline = false,
            };

            Response.Headers.Add("Content-Disposition", contentDisposition.ToString());

            return File(file.Data, file.ContentType, file.FileName);
        }

        protected Guid UserId
        {
            get
            {
                if (User == null || !User.Identity.IsAuthenticated)
                {
                    throw new AuthenticationException("User not login to system");
                }

                var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

                Guid userId;
                if (!Guid.TryParse(userIdClaim, out userId))
                {
                    throw new AuthenticationException("User not login to system");
                }

                return userId;
            }
        }

        protected Guid? GetUserId()
        {
            var userIdClaim = User?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                return null;
            }

            return userId;
        }
    }
}