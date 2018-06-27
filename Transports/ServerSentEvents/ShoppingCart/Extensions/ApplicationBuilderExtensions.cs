using ShoppingCart.Middlewares;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseServerSentEvents(this IApplicationBuilder app) =>
            app.MapWhen(
                context => 
                    context.Request.Query.ContainsKey("id") 
                    && context.Request.Path == "/sse"
                    && context.Request.Headers["Accept"][0] == "text/event-stream",
                configuration => configuration.UseMiddleware<ServerSentEventsMiddleware>());
    }
}
