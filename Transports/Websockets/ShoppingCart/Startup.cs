using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.HostedServices;
using ShoppingCart.Repositories;
using ShoppingCart.Services;
using System;

namespace ShoppingCart
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<IOrderRepository, OrderRepository>()
                .AddSingleton<ICheckoutService, CheckoutService>()
                .AddSingleton<IOrderService, OrderService>()
                .AddHostedService<UpdateOrderStatusHostedService>()
                .AddMvcCore()
                .AddJsonFormatters();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var webSocketsOptions = new WebSocketOptions
            {
                KeepAliveInterval = TimeSpan.FromSeconds(120),
                ReceiveBufferSize = 4 * 1024
            };

            app
                .UseWebSockets(webSocketsOptions)
                .UseOrderStatusWebSocket()
                .UseDefaultFiles()
                .UseStaticFiles()
                .UseMvc();
        }
    }
}
