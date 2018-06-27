using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using ShoppingCart.Model;
using ShoppingCart.Services;
using System;
using System.Threading.Tasks;

namespace ShoppingCart.Middlewares
{
    public class ServerSentEventsMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IOrderService orderService;

        public ServerSentEventsMiddleware(RequestDelegate next, IOrderService orderService)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        public async Task Invoke(HttpContext context)
        {
            int status;
            var response = context.Response;
            context.Request.Query.TryGetValue("id", out StringValues values);
            response.ContentType = "text/event-stream";
            do
            {
                status = (int)orderService.GetOrderStatus(values[0]);
                await response.WriteAsync($"data: {status}\r\r");
                response.Body.Flush();
                await Task.Delay(5000);
            } while (status != (int)OrderStatus.Shipped);

            response.Body.Close();
        }
    }
}
