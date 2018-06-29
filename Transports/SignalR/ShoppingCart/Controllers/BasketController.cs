using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ShoppingCart.Hubs;
using ShoppingCart.Model;
using ShoppingCart.Services;
using System;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController
    {
        private readonly ICheckoutService checkoutService;
        private readonly IHubContext<OrderStatusHub> hub;

        public BasketController(
            ICheckoutService checkoutService,
            IHubContext<OrderStatusHub> hub)
        {
            this.checkoutService = checkoutService ?? throw new ArgumentNullException(nameof(checkoutService));
            this.hub = hub ?? throw new ArgumentNullException(nameof(hub));
        }

        [Route("checkout")]
        [HttpPost]
        public async Task<ActionResult<Checkout>> Checkout(Basket basket)
        {
            await hub.Clients.All.SendAsync("Checkout", basket);

            return checkoutService.Checkout(basket);
        }
    }
}
