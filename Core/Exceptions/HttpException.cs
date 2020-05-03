using System;
using System.Net;

namespace Kaizen.Core.Exceptions
{
    public class HttpException : Exception
    {
        public HttpException() : this ((int)HttpStatusCode.InternalServerError, "Internal server error")
        {

        }
        
        public HttpException(int statusCode) : this(statusCode, "An error ocurred")
        {
            
        }

        public HttpException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; set; }
    }
}
