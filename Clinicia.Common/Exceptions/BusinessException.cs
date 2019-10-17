using System;

namespace Clinicia.Common.Exceptions
{
    public class BusinessException : Exception
    {
        public string ErrorCode { get; set; }

        public BusinessException(string errorCode, string errorMessage)
            : base(errorMessage)
        {
            ErrorCode = errorCode;
        }
    }
}