using API.Services;
using CafeLinkAPI.Interfaces;

namespace CafeLinkAPI.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration config)
        {
            
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}