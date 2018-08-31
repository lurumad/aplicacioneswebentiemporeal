using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using WhatsApp.Controllers;
using WhatsApp.Hubs;
using WhatsApp.Middlewares;
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
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                        {
                            ValidIssuer = WhatsAppController.Issuer,
                            ValidAudience = WhatsAppController.Audience,
                            IssuerSigningKey = WhatsAppController.SigningCredentials.Key
                        };

                        options.Events = new JwtBearerEvents
                        {
                            OnMessageReceived = context =>
                            {
                                var accessToken = context.Request.Query["access_token"];

                                if (!string.IsNullOrEmpty(accessToken) &&
                                    (context.HttpContext.WebSockets.IsWebSocketRequest || context.Request.Headers["Accept"] == "text/event-stream"))
                                {
                                    context.Token = context.Request.Query["access_token"];
                                }
                                return Task.CompletedTask;
                            }
                        };
                    })
                .Services
                .AddMvcCore()
                .AddJsonFormatters(options => options.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app
                .UseWebSockets()
                //.UseMiddleware<AccessTokenMiddleware>()
                .UseAuthentication()
                .UseDefaultFiles()
                .UseStaticFiles()
                .UseMvc()
                .UseSignalR(routes => routes.MapHub<WhatsAppHub>("/whatsapp"));
        }
    }
}
