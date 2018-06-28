using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using ShoppingCart.Model;
using ShoppingCart.Services;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingCart.Middlewares
{
    public class OrderStatusWebSocketMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IOrderService orderService;

        public OrderStatusWebSocketMiddleware(RequestDelegate next, IOrderService orderService)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        public async Task Invoke(HttpContext context)
        {
            var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            var buffer = new byte[4 * 1024];
            int status;
            var response = context.Response;
            context.Request.Query.TryGetValue("id", out StringValues values);
            do
            {
                status = (int)orderService.GetOrderStatus(values[0]);
                var array = Encoding.ASCII.GetBytes(status.ToString());
                await webSocket.SendAsync(
                    new ArraySegment<byte>(array, 0, array.Length),
                    WebSocketMessageType.Text,
                    endOfMessage: true,
                    cancellationToken: CancellationToken.None);
                await Task.Delay(5000);
            } while (status != (int)OrderStatus.Shipped);

            await webSocket.CloseAsync(
                WebSocketCloseStatus.NormalClosure,
                "Order has been shipped!",
                CancellationToken.None);
        }
    }
}
