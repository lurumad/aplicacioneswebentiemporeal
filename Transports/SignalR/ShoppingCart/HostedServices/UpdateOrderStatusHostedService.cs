using Microsoft.Extensions.Hosting;
using ShoppingCart.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingCart.HostedServices
{
    public class UpdateOrderStatusHostedService : IHostedService
    {
        private CancellationTokenSource shutdown = new CancellationTokenSource();
        private Task backgroundTask;
        private readonly IOrderService orderService;

        public UpdateOrderStatusHostedService(IOrderService orderService)
        {
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
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
                    orderService.ChangeStatus(orderId);
                }

                await Task.Delay(5000);
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
