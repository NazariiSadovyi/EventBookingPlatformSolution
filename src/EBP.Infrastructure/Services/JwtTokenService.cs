using EBP.Application.Interfaces;
using EBP.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EBP.Infrastructure.Services
{
    public  class JwtTokenService(IOptions<JwtOptions> _options) : ITokenService
    {
        public string CreateJwt(string userId, string email, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(ClaimTypes.NameIdentifier, userId),
            };

            claims.AddRange(roles.Select(_ => new Claim(ClaimTypes.Role, _)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _options.Value.Issuer,
                audience: _options.Value.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_options.Value.ExpiresHours),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
