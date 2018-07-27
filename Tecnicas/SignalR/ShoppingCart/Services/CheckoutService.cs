using ShoppingCart.Model;
using ShoppingCart.Repositories;

namespace ShoppingCart.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IOrderRepository orderRepository;

        public CheckoutService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository ?? throw new System.ArgumentNullException(nameof(orderRepository));
        }

        public Checkout Checkout(Basket basket)
        {
            var orderId = orderRepository.CreateOrder(basket);
            return new Checkout { OrderId = orderId };
        }
    }
}
