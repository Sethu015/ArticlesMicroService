using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using EmailService.Contracts;
using Blocks.Core.Extensions;

namespace EmailService.Smtp
{
    public static class AddSmtpMailServices
    {
        public static IServiceCollection AddSmtpEmailService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAndValidateOptions<SmtpOptions>(configuration);
            services.AddSingleton<IEmailService, EmailService>();
            return services;
        }
    }
}
