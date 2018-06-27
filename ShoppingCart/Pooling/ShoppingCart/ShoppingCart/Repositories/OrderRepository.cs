﻿using ShoppingCart.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private ConcurrentDictionary<string, OrderStatus> orders =
            new ConcurrentDictionary<string, OrderStatus>();

        public void ChangeStatus(string id)
        {
            if (orders.TryGetValue(id, out OrderStatus currentStatus))
            {
                orders.TryUpdate(id, currentStatus.Next(), currentStatus);
            }
        }

        public string CreateOrder(Basket basket)
        {
            var id = Guid.NewGuid().ToString();
            orders.TryAdd(id, OrderStatus.Submitted);
            return id;
        }

        public IEnumerable<string> GetOrderIds()
        {
            return orders
                .Where(kp => kp.Value != OrderStatus.Shipped)
                .Select(kp => kp.Key);
        }

        public OrderStatus GetOrderStatus(string id)
        {
            orders.TryGetValue(id, out OrderStatus status);
            return status;
        }
    }
}
