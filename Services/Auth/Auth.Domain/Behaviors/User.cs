using Auth.Domain.Users.ValueObjects;
using Blocks.Core.Extensions;

namespace Auth.Domain.Users
{
    public partial class User
    {
        public static User Create(ICreateUser createUser)
        {
            if(createUser.UserRoles.IsNullOrEmpty())
                throw new ArgumentException($"User must have at least one role assigned.",nameof(createUser.UserRoles));

            User user = new User
            {
                UserName = createUser.Email,
                Email = createUser.Email,
                FirstName = createUser.FirstName,
                LastName = createUser.LastName,
                Gender = createUser.Gender,
                Honorific = HonorificTitle.FromEnum(createUser.Honorific),
                PhoneNumber = createUser.PhoneNumber,
                PictureUrl = createUser.ProfilePictureUrl,
                ProfessionalProfile = ProfessionalProfile.Create(createUser.Position, createUser.CompanyName, createUser.Affiliation),
                _userRoles = createUser.UserRoles.Select(r => UserRole.Create(r)).ToList()
            };

            return user;
        }

        public void AddRefreshToken(RefreshToken refreshToken)
        {
            _refreshTokens.Add(refreshToken);
        }
    }
}
