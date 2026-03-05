using Microsoft.Extensions.DependencyInjection;

namespace Auth.Application
{
    public static class DependenciesConfiguration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<TokenFactory>();
            return services;
        }
    }
}
