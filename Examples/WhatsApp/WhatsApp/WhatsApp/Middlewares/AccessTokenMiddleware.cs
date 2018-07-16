using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WhatsApp.Middlewares
{
    public class AccessTokenMiddleware
    {
        private readonly RequestDelegate next;

        public AccessTokenMiddleware(RequestDelegate next)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                if (context.Request.QueryString.HasValue 
                    && context.Request.Query.TryGetValue("access_token", out StringValues token))
                {
                    context.Request.Headers.Add(HeaderNames.Authorization, $"Bearer {token.FirstOrDefault()}");
                }
            }

            return next(context);
        }
    }
}
