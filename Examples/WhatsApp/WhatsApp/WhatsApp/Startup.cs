using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WhatsApp.Controllers;
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
                .AddAuthorization()
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(option =>
                    {
                        option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                        {
                            ValidIssuer = WhatsAppController.Issuer,
                            ValidAudience = WhatsAppController.Audience,
                            IssuerSigningKey = WhatsAppController.SigningCredentials.Key
                        };
                    })
                .Services
                .AddMvcCore()
                .AddJsonFormatters(options => options.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app
                .UseAuthentication()
                .UseDefaultFiles()
                .UseStaticFiles()
                .UseMvc()
                .UseSignalR(routes => routes.MapHub<WhatsAppHub>("/whatsapp"));
        }
    }
}
