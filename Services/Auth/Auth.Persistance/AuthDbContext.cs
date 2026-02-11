using Auth.Domain.Roles;
using Auth.Domain.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auth.Persistance
{
    public class AuthDbContext(DbContextOptions<AuthDbContext> dbContextOptions) : IdentityDbContext<User, Role, int>(dbContextOptions)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
