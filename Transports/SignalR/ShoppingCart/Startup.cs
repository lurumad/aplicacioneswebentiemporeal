using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.HostedServices;
using ShoppingCart.Hubs;
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
                .AddJsonFormatters()
                .Services
                .AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app
                .UseDefaultFiles()
                .UseStaticFiles()
                .UseSignalR(routes => routes.MapHub<OrderStatusHub>("/orderstatus"))
                .UseMvc();
        }
    }
}
