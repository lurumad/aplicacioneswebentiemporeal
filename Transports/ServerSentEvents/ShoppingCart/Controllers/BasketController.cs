using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Model;
using ShoppingCart.Services;
using System;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController
    {
        private readonly ICheckoutService checkoutService;

        public BasketController(ICheckoutService checkoutService)
        {
            this.checkoutService = checkoutService ?? throw new ArgumentNullException(nameof(checkoutService));
        }

        [Route("checkout")]
        [HttpPost]
        public ActionResult<Checkout> Checkout(Basket basket)
        {
            return checkoutService.Checkout(basket);
        }
    }
}
