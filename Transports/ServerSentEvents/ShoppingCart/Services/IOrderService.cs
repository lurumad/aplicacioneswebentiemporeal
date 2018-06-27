using ShoppingCart.Model;
using System.Collections.Generic;

namespace ShoppingCart.Services
{
    public interface IOrderService
    {
        OrderStatus GetOrderStatus(string id);
        void ChangeStatus(string id);
        IEnumerable<string> GetOrdersIds();
    }
}