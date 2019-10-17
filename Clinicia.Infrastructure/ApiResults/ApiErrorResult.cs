namespace Clinicia.Infrastructure.ApiResults
{
    public class ApiErrorResult : ApiResult
    {
        public string ErrorCode { get; set; }

        public object ErrorMessage { get; set; }

        public string TechnicalLog { get; internal set; }
    }
}