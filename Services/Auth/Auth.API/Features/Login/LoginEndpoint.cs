using Articles.Security;
using Blocks.Core.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.API.Features.Login
{
    [HttpPost("login")]
    public class LoginEndpoint(UserManager<User> _userManager, SignInManager<User> _signInManager, IOptions<JwtOptions> _options) : Endpoint<LoginCommand, LoginResponse>
    {
        public override async Task HandleAsync(LoginCommand command, CancellationToken ct)
        {
            var user = await _userManager.FindByEmailAsync(command.Email);
            if (user is null)
                throw new BadRequestException($"User not Found {command.Email}");
            var result = await _signInManager.PasswordSignInAsync(user,command.Password, false, false);
            if(!result.Succeeded)
                throw new BadRequestException($"Invalid Credentials for {command.Email}");
            var roles = await _userManager.GetRolesAsync(user);
            var jwtToken = GenerateJWTToken(user.Id.ToString(), command.Email, user.UserName, roles, Array.Empty<Claim>());
        }

        public string GenerateJWTToken(string userId, string email, string userName, IEnumerable<string> roles, IEnumerable<Claim> additionalClaims)
        {
            var options = _options.Value;
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,userId),
                new Claim(JwtRegisteredClaimNames.Email,email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToUnixEpochDate().ToString(),ClaimValueTypes.Integer64),

                new Claim(ClaimTypes.Name,userName)
            }
            .Concat(roles.Select(r => new Claim(ClaimTypes.Role, r)))
            .Concat(additionalClaims);

            var symmentricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Secret));
            var signInCreds = new SigningCredentials(symmentricKey, SecurityAlgorithms.HmacSha512);

            var jwt = new JwtSecurityToken(
                issuer: options.Issuer,
                audience: options.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: options.Expiration,
                signingCredentials: signInCreds);

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedToken;
        }
    }
}
