using Clinicia.Infrastructure.ApiControllers;
using Microsoft.AspNetCore.Mvc;

namespace Clinicia.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class ValuesController : BaseApiController
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Success(new string[] { "value1", "value2" });
        }
    }
}