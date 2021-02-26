using Microsoft.AspNetCore.Http;

namespace Kaizen.Middleware
{
    public static class KaizenHttpContext
    {
        private static IHttpContextAccessor _httpContextAccessor;
        private static HttpContext Current => _httpContextAccessor.HttpContext;

        public static string BaseUrl => $"{Current.Request.Scheme}://{Current.Request.Host}{Current.Request.PathBase}";

        public static void Configure(IHttpContextAccessor contextAccessor)
        {
            _httpContextAccessor = contextAccessor;
        }
    }
}
