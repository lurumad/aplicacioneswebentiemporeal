using System.Collections.Generic;
using ShoppingCart.Model;

namespace ShoppingCart.Repositories
{
    public interface IOrderRepository
    {
        string CreateOrder(Basket basket);
        OrderStatus GetOrderStatus(string id);
        void ChangeStatus(string id);
        IEnumerable<string> GetOrderIds();
    }
}