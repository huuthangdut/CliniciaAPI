using Clinicia.Infrastructure.ApiControllers;
using Clinicia.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Clinicia.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class ValuesController : BaseApiController
    {
        public IUserService PatientService { get; }

        public ValuesController(IUserService patientService)
        {
            PatientService = patientService;
        }
    }
}