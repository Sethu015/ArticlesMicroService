using Articles.Security;
using Auth.Domain.Roles;
using Auth.Domain.Users;
using Auth.Persistance;
using Blocks.Core.Extensions;
using EmailService.Smtp;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Auth.API
{
    public static class DependenciesConfiguration
    {
        public static IServiceCollection ConfigureApiOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAndValidateOptions<JwtOptions>(configuration);
            return services;
        }

        public static IServiceCollection ConfigureApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddFastEndpoints()
                .SwaggerDocument()
                .AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .AddJwtAuthentication(configuration)
                .AddJwtIdentity(configuration)
                .AddAuthorization();

            services.AddSmtpEmailService(configuration);
            return services;
        }

        public static IServiceCollection AddJwtIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityCore<User>(options =>
            {
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
            })
            .AddRoles<Role>()
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddSignInManager<SignInManager<User>>()
            .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.RoleClaimType = ClaimTypes.Role;
            });


            return services;
        }
    }
}
