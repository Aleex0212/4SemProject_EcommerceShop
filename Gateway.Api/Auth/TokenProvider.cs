using System.Security.Claims;
using System.Text;
using EcommerceShop.Common.Dto;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Gateway.Api.Auth
{
  public class TokenProvider(IConfiguration configuration)
  {
    public string Create(LoginDto login)
    {
      string secretKey = configuration["Jwt:Secret"];
      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

      var tokenDescriptor = new SecurityTokenDescriptor()
      {
        Subject = new ClaimsIdentity(
        [
          new Claim(JwtRegisteredClaimNames.Email, Convert.ToString(login.Email)),
        ]),

        Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
        SigningCredentials = credentials,
        Issuer = configuration["Jwt:Issuer"],
        Audience = configuration["Jwt:Audience"]
      };

      var handler = new JsonWebTokenHandler();
      string token = handler.CreateToken(tokenDescriptor);

      return token;
    }
  }
}
