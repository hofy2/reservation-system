using Microsoft.Extensions.Primitives;

namespace Web
{
    public class SecurityHeadersMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
    {
        private readonly RequestDelegate _next = next;
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Response.Headers.ContainsKey("referrer-policy"))
            {
                // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Referrer-Policy
                context.Response.Headers["referrer-policy"] = new StringValues("strict-origin-when-cross-origin");
            }

            if (!context.Response.Headers.ContainsKey("x-content-type-options"))
            {
                // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Content-Type-Options
                context.Response.Headers["x-content-type-options"] = new StringValues("nosniff");
            }

            if (!context.Response.Headers.ContainsKey("x-xss-protection"))
            {
                // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-XSS-Protection
                context.Response.Headers["x-xss-protection"] = new StringValues("1; mode=block");
            }

            await _next.Invoke(context);
        }
    }
}