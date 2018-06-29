using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Model;
using ShoppingCart.Services;
using System;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        [Route("{id}")]
        [HttpGet]
        public async Task GetOrderStatus(string id)
        {
            int status;
            Response.Headers.Add("Content-Type", "text/event-stream");
            do
            {
                status = (int)orderService.GetOrderStatus(id);
                await Response.WriteAsync($"data: {status}\r\r");
                Response.Body.Flush();
                await Task.Delay(5000);
            } while (status != (int)OrderStatus.Shipped);

            Response.Body.Close();
        }
    }
}
