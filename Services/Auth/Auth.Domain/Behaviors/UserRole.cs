using Mapster;

namespace Auth.Domain.Users
{
    public partial class UserRole
    {
        public static UserRole Create(IUserRole userRole)
        {
            var now = DateTime.UtcNow.Date;

            if (userRole.StartDate.HasValue && userRole.StartDate.Value.Date < now)
                throw new ArgumentException($"Start date cannot be in the past.", nameof(userRole.StartDate));

            if(userRole.StartDate.HasValue && userRole.EndDate.HasValue
                && userRole.StartDate.Value.Date > userRole.EndDate.Value.Date)
                    throw new ArgumentException($"Expiring Date Must be after start date",nameof(userRole.StartDate));

            var userRoleTobeReturned = userRole.Adapt<UserRole>();
            return userRoleTobeReturned;
        }
    }
}
