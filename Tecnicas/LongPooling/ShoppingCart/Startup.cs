using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.HostedServices;
using ShoppingCart.Repositories;
using ShoppingCart.Services;

namespace ShoppingCart
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMemoryCache()
                .AddSingleton<IOrderRepository, OrderRepository>()
                .AddSingleton<ICheckoutService, CheckoutService>()
                .AddSingleton<IOrderService, OrderService>()
                .AddHostedService<UpdateOrderStatusHostedService>()
                .AddMvcCore()
                .AddJsonFormatters();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app
                .UseDefaultFiles()
                .UseStaticFiles()
                .UseMvc();
        }
    }
}
