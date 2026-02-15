using Auth.Domain.Users;

namespace Auth.Domain.Events
{
    public record UserCreated(User User, string ResetPasswordToken);
}
