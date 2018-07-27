using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.HostedServices;
using ShoppingCart.Hubs;
using ShoppingCart.Repositories;
using ShoppingCart.Services;

namespace ShoppingCart
{
    public class Startup
    {
        private const string Path = "orderstatus";

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMemoryCache()
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
                .UseSignalR(routes => routes.MapHub<ShoppingCartHub>(path: $"/{Path}"))
                .UseMvc();
        }
    }
}
