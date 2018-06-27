using ShoppingCart.Model;

namespace ShoppingCart.Services
{
    public interface ICheckoutService
    {
        Checkout Checkout(Basket basket);
    }
}