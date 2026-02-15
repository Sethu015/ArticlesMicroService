
using Articles.Abstractions.Enums;
using Auth.Domain.Users.Enums;

namespace Auth.Domain
{
    public interface ICreateUser
    {
        string? Affiliation { get; }
        string? CompanyName { get; }
        string Email { get; }
        string FirstName { get; }
        Gender Gender { get; }
        Honorific? Honorific { get; }
        string LastName { get; }
        string? PhoneNumber { get; }
        string? Position { get; }
        string? ProfilePictureUrl { get; }
        IReadOnlyList<IUserRole> UserRoles { get; }
    }

    public interface IUserRole
    {
        DateTime? EndDate { get; }
        DateTime? StartDate { get; }
        UserRoleType UserRole { get; }
    }
}