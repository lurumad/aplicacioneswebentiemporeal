using Microsoft.AspNetCore.SignalR;
using ShoppingCart.Model;
using ShoppingCart.Services;
using System.Threading.Tasks;

namespace ShoppingCart.Hubs
{
    public class OrderStatusHub : Hub
    {
        private readonly IOrderService orderService;

        public OrderStatusHub(IOrderService orderService)
        {
            this.orderService = orderService ?? throw new System.ArgumentNullException(nameof(orderService));
        }

        public async Task GetOrderStatus(string id)
        {
            int status;
            do
            {
                status = (int)orderService.GetOrderStatus(id);
                await Clients.Caller.SendAsync("UpdateOrderStatus", status);
                await Task.Delay(5000);
            } while (status != (int)OrderStatus.Shipped);
        }
    }
}
