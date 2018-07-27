using ShoppingCart.Model;
using ShoppingCart.Repositories;
using System;
using System.Collections.Generic;

namespace ShoppingCart.Services
{
    public partial class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public OrderStatus ChangeStatus(string id)
        {
            return orderRepository.ChangeStatus(id);
        }

        public IEnumerable<string> GetOrdersIds()
        {
            return orderRepository.GetOrderIds();
        }

        public OrderStatus GetOrderStatus(string id)
        {
            return orderRepository.GetOrderStatus(id);
        }
    }
}
