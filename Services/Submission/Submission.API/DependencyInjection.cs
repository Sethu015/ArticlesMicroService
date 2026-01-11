namespace Submission.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMemoryCache() // In-memory caching
                .AddSwaggerGen() // Swagger API documentation
                .AddEndpointsApiExplorer(); // API endpoint exploration
            return services;
        }
    }
}
