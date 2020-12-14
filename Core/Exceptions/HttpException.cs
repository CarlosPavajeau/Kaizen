using System;
using System.Net;
using System.Runtime.Serialization;

namespace Kaizen.Core.Exceptions
{
    [Serializable]
    public class HttpException : Exception
    {
        public HttpException() : this((int)HttpStatusCode.InternalServerError, "Internal server error")
        {
        }

        public HttpException(int statusCode) : this(statusCode, "An error ocurred")
        {
        }

        public HttpException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        protected HttpException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int StatusCode { get; set; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
