using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WhatsApp.Hubs;
using WhatsApp.Repositories;
using WhatsApp.Services;

namespace WhatsApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<IChatRepository, ChatRepository>()
                .AddSingleton<IChatService, ChatService>()
                .AddSignalR()
                .Services
                .AddMvcCore()
                .AddJsonFormatters(options => options.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app
                .UseDefaultFiles()
                .UseStaticFiles()
                .UseSignalR(routes => routes.MapHub<WhatsAppHub>("/whatsapp"))
                .UseMvc();
        }
    }
}
