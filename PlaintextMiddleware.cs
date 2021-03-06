﻿using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace KestrelHttpsPerf
{
    public class PlaintextMiddleware
    {
        private static readonly PathString _path = new PathString("/plaintext");
        private static readonly byte[] _helloWorldPayload = Encoding.UTF8.GetBytes("Hello, World!");

        private readonly RequestDelegate _next;

        public PlaintextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.StartsWithSegments(_path, StringComparison.Ordinal))
            {
                httpContext.Response.StatusCode = 200;
                httpContext.Response.ContentType = "text/plain";
                // HACK: Setting the Content-Length header manually avoids the cost of serializing the int to a string.
                //       This is instead of: httpContext.Response.ContentLength = _helloWorldPayload.Length;
                httpContext.Response.Headers["Content-Length"] = "13";
                return httpContext.Response.Body.WriteAsync(_helloWorldPayload, 0, _helloWorldPayload.Length);
            }

            return _next(httpContext);
        }
    }

    public static class PlaintextMiddlewareExtensions
    {
        public static IApplicationBuilder UsePlainText(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PlaintextMiddleware>();
        }
    }
}