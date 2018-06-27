using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ShoppingCart.Model;
using ShoppingCart.Services;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController
    {
        private readonly IOrderService orderService;
        private readonly IMemoryCache cache;

        public OrdersController(IOrderService orderService, IMemoryCache cache)
        {
            this.orderService = orderService ?? throw new System.ArgumentNullException(nameof(orderService));
            this.cache = cache ?? throw new System.ArgumentNullException(nameof(cache));
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<OrderStatus>> GetOrderStatus(string id)
        {
            OrderStatus currentOrderStatus;
            OrderStatus previousOrderStatus;

            do
            {
                currentOrderStatus = orderService.GetOrderStatus(id);
                previousOrderStatus = cache.Get<OrderStatus>(id);
                await Task.Delay(2000);
            } while (currentOrderStatus == previousOrderStatus);

            cache.Set(id, currentOrderStatus);
            return currentOrderStatus;
        }
    }
}
