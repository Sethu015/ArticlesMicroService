using Articles.Abstractions.Enums;
using Auth.Domain;
using Auth.Domain.Users.Enums;

namespace Auth.API.Features.CreateUser
{
    public class CreateUserCommand : ICreateUser
    {
        public required string Email { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required Gender Gender { get; init; }
        public Honorific? Honorific { get; init; }

        public string? PhoneNumber { get; init; }
        public string? ProfilePictureUrl { get; init; }

        public string? CompanyName { get; init; }
        public string? Position { get; init; }
        public string? Affiliation { get; init; }
        public required IReadOnlyList<UserRoleDto> UserRoles { get; init; } = new List<UserRoleDto>();
        IReadOnlyList<IUserRole> ICreateUser.UserRoles { get => UserRoles; }
    }

    public record UserRoleDto(
        UserRoleType UserRole,
        DateTime? StartDate,
        DateTime? EndDate) : IUserRole;

    public record CreateUserResponse(
        string Email,
        int UserId,
        string Token);
}
