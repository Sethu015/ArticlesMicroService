using Articles.Security;
using Auth.Domain.Users;
using Blocks.Core.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Auth.Application
{
    public class TokenFactory
    {
        private readonly JwtOptions _options;

        public TokenFactory(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public RefreshToken GenerateRefreshToken(string clientIpAddress)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var randomBytes = new byte[64];
                rng.GetBytes(randomBytes);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    CreatedOn = DateTime.UtcNow,
                    ExpiresOn = DateTime.UtcNow.AddDays(7),
                    CreatedByIp = clientIpAddress
                };
            }
        }

        public string GenerateJWTToken(string userId, string email, string userName, IEnumerable<string> roles, IEnumerable<Claim> additionalClaims)
        {
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

            var symmentricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret));
            var signInCreds = new SigningCredentials(symmentricKey, SecurityAlgorithms.HmacSha512);

            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: _options.Expiration,
                signingCredentials: signInCreds);

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedToken;
        }

    }
}
