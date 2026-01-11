using Articles.Abstractions.Enums;
using Microsoft.AspNetCore.Builder;

namespace Articles.Security;
public static class Extensions
{
    public static TBuilder RequireRoleAuthorization<TBuilder>(this TBuilder builder, params string[] roles)
        where TBuilder : IEndpointConventionBuilder
    {
        return builder.RequireAuthorization(policy => policy.RequireRole(roles));
    }

    public static TBuilder RequireRoleAuthorization<TBuilder>(this TBuilder builder, params UserRoleType[] roles)
        where TBuilder : IEndpointConventionBuilder
    {
        var roleNames = roles.Select(r => r.ToString()).ToArray();
        return builder.RequireAuthorization(policy => policy.RequireRole(roleNames));
    }
}
