using InterviewApp.Models;
using InterviewApp.Services.Greeting;
using InterviewApp.Services.TimeGreeting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InterviewApp.Services
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            services.Configure<GreetingOptions>(
                configuration.GetSection("Greeting"));

            services.AddTransient<IGreetingService, GreetingService>();
            services.AddTransient<ITimeGreetingService, TimeGreetingService>();

            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(ServiceExtension).Assembly));

            return services;
        }
    }
}

