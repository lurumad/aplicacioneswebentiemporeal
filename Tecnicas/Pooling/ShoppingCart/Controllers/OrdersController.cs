using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Model;
using ShoppingCart.Services;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService ?? 
                throw new System.ArgumentNullException(nameof(orderService));
        }

        [Route("{id}")]
        [HttpGet]
        public ActionResult<OrderStatus> GetOrderStatus(string id)
        {
            return orderService.GetOrderStatus(id);
        }
    }
}
