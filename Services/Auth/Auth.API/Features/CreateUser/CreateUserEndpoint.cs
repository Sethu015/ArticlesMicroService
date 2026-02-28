using Articles.Abstractions.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Auth.API.Features.CreateUser
{
    [Authorize(Roles = Role.USERADMIN)]
    [HttpPost("users")]
    public class CreateUserEndpoint(UserManager<User> _userManager) : Endpoint<CreateUserCommand, CreateUserResponse>
    {
        public override async Task HandleAsync(CreateUserCommand req, CancellationToken ct)
        {
            var user = await _userManager.FindByEmailAsync(req.Email);
            if (user is not null)
                throw new BadRequestException($"User with email {req.Email} already exists.");

            var userToBeCreated = Auth.Domain.Users.User.Create(req);

            var result = await _userManager.CreateAsync(userToBeCreated);
            if (!result.Succeeded)
            {
                var errorMessage = string.Join(" | ", result.Errors.Select(res => $"{res.Code} : {res.Description}"));
                throw new BadRequestException(errorMessage);
            }

            var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(userToBeCreated);

            await PublishAsync(new UserCreated(user, resetPasswordToken));

            await Send.OkAsync(new CreateUserResponse(user.Email,userToBeCreated.Id,resetPasswordToken), cancellation: ct);
        }
    }
}
