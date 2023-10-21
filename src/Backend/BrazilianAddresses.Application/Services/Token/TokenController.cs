using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BrazilianAddresses.Application.Services.Token
{
    public class TokenController
    {
        private readonly string _emailAlias = "eml";

        private readonly double _expirationTime;

        private readonly string _securityPassword;

        public TokenController(double expirationTime, string securityPassword)
        {
            _expirationTime = expirationTime;
            _securityPassword = securityPassword;
        }

        public string GenerateTokenJwt(string userEmail)
        {
            List<Claim> claimList = new List<Claim>
            {
                new Claim(_emailAlias, userEmail)
            };

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.UtcNow.AddHours(_expirationTime),
                SigningCredentials = new SigningCredentials(SymmetricKey(), SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(tokenDescriptor);

            return jwtSecurityTokenHandler.WriteToken(securityToken);
        }

        public ClaimsPrincipal ValidateTokenJwt(string tokenJwt)
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                RequireExpirationTime = true,
                IssuerSigningKey = SymmetricKey(),
                ClockSkew = new TimeSpan(0),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            ClaimsPrincipal claimsPrincipal = jwtSecurityTokenHandler.ValidateToken(tokenJwt, validationParameters, out _);

            return claimsPrincipal;
        }

        public string GetEmailFromTokenJwt(string tokenJwt)
        {
            ClaimsPrincipal claimsPrincipal = ValidateTokenJwt(tokenJwt);

            return claimsPrincipal.FindFirst(_emailAlias).Value;
        }

        private SymmetricSecurityKey SymmetricKey()
        {
            byte[] symmetricKey = Convert.FromBase64String(_securityPassword);

            return new SymmetricSecurityKey(symmetricKey);
        }
    }
}
