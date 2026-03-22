using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Fast.Workshops.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Fast.Workshops.Application.Services
{
    public class TokenService
    {
        private readonly string _secretKey;
        private const int TokenExpirationHours = 2;
        private const string SecretKeyConfigurationPath = "JWT:SECRETKEY";

        public TokenService(IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration);

            _secretKey = configuration[SecretKeyConfigurationPath]
                            ?? throw new InvalidOperationException("JWT secret key is not configured");
        }

        public string GenerateToken(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var tokenDescriptor = CreateTokenDescriptor(user, key);
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private SecurityTokenDescriptor CreateTokenDescriptor(User user, byte[] key)
        {
            return new SecurityTokenDescriptor
            {
                Subject = CreateClaimsIdentity(user),
                Expires = DateTime.UtcNow.AddHours(TokenExpirationHours),
                SigningCredentials = CreateSigningCredentials(key)
            };
        }

        private static ClaimsIdentity CreateClaimsIdentity(User user)
        {
            return new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name)
        });
        }

        private static SigningCredentials CreateSigningCredentials(byte[] key)
        {
            return new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);
        }
    }
}
