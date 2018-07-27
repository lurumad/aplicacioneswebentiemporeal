using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using ShoppingCart.Hubs;
using ShoppingCart.Model;
using ShoppingCart.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingCart.HostedServices
{
    public class UpdateOrderStatusHostedService : IHostedService
    {
        private readonly CancellationTokenSource shutdown = new CancellationTokenSource();
        private readonly IHubContext<ShoppingCartHub> hub;
        private readonly IMemoryCache cache;
        private readonly IOrderService orderService;
        private Task backgroundTask;

        public UpdateOrderStatusHostedService(
            IOrderService orderService,
            IHubContext<ShoppingCartHub> hub,
            IMemoryCache cache)
        {
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            this.hub = hub ?? throw new ArgumentNullException(nameof(hub));
            this.cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            backgroundTask = Task.Run(DoWork);

            return Task.CompletedTask;
        }

        private async Task DoWork()
        {
            while (!shutdown.IsCancellationRequested)
            {
                var ordersIds = orderService.GetOrdersIds();

                foreach (var orderId in ordersIds)
                {
                    var connectionId = cache.Get<string>(orderId);
                    var status = orderService.ChangeStatus(orderId);
                    await hub.Clients.Client(connectionId).SendAsync("UpdateOrderStatus", status);

                    if (status == OrderStatus.Shipped)
                    {
                        cache.Remove(orderId);
                    }
                }

                await Task.Delay(1000);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            shutdown.Cancel();

            return Task.WhenAny(
                backgroundTask,
                Task.Delay(Timeout.Infinite, cancellationToken));
        }
    }
}
