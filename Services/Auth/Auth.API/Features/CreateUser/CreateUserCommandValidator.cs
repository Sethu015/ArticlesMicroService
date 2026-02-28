namespace Auth.API.Features.CreateUser
{
    public class CreateUserCommandValidator : Validator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.FirstName)
                .NotEmpty();
            RuleFor(x => x.LastName)
                .NotEmpty();

            RuleFor(x => x.UserRoles).NotEmpty()
                .Must((c, roles) => AreUserRoleDatesValid(roles)).WithMessage("Invalid Role");
        }

        private static bool AreUserRoleDatesValid(IReadOnlyList<UserRoleDto> roles)
        {
            return roles.All(r =>
                (!r.StartDate.HasValue || r.StartDate.Value.Date >= DateTime.UtcNow.Date) &&
                (!r.EndDate.HasValue || (r.StartDate ?? DateTime.UtcNow) < r.EndDate.Value.Date)
                );

        }
    }
}
