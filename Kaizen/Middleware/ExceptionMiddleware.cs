using System;
using System.Net;
using System.Threading.Tasks;
using Kaizen.Core.Exceptions;
using Kaizen.Models.Error;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Kaizen.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(httpContext, exception);
            }
        }

        public Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";

            if (exception is HttpException httpException)
            {
                httpContext.Response.StatusCode = httpException.StatusCode;
                return httpContext.Response.WriteAsync(new ErrorDetail
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Message = exception.Message
                }.ToString());
            }
            else
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return httpContext.Response.WriteAsync(JsonConvert.SerializeObject(exception));
            }
        }
    }
}
