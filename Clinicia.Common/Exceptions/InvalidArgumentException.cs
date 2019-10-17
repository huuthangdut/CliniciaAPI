using System;

namespace Clinicia.Common.Exceptions
{
    public class InvalidArgumentException : Exception
    {
        public InvalidArgumentException() : base("Invalid parameter(s)")
        {
        }

        public InvalidArgumentException(string errorMessage) : base(errorMessage)
        {
        }
    }
}