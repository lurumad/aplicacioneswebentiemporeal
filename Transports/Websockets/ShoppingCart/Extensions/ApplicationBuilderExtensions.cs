using ShoppingCart.Middlewares;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseOrderStatusWebSocket(this IApplicationBuilder app) =>
            app.MapWhen(
                context => 
                    context.Request.Query.ContainsKey("id") 
                    && context.Request.Path == "/ws"
                    && context.WebSockets.IsWebSocketRequest,
                configuration => configuration.UseMiddleware<OrderStatusWebSocketMiddleware>());
    }
}
