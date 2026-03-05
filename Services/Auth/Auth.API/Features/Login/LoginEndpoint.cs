using System.Security.Claims;
using Blocks.AspNetCore.Extensions;
using Auth.Application;

namespace Auth.API.Features.Login
{
    [HttpPost("login")]
    public class LoginEndpoint(UserManager<User> _userManager, SignInManager<User> _signInManager,TokenFactory tokenFactory) : Endpoint<LoginCommand, LoginResponse>
    {
        private readonly TokenFactory _tokenFactory = tokenFactory;

        public override async Task HandleAsync(LoginCommand command, CancellationToken ct)
        {
            var user = await _userManager.FindByEmailAsync(command.Email);
            if (user is null)
                throw new BadRequestException($"User not Found {command.Email}");
            var result = await _signInManager.PasswordSignInAsync(user,command.Password, false, false);
            if(!result.Succeeded)
                throw new BadRequestException($"Invalid Credentials for {command.Email}");
            var roles = await _userManager.GetRolesAsync(user);
            var jwtToken = _tokenFactory.GenerateJWTToken(user.Id.ToString(), command.Email, user.UserName, roles, Array.Empty<Claim>());
            var refreshToken = _tokenFactory.GenerateRefreshToken(HttpContext.GetClientIpAddress());
            user.AddRefreshToken(refreshToken);
            await Send.OkAsync(new LoginResponse(user.Email, jwtToken, refreshToken.Token));
        }

    }
}
