using System.Security.Claims;
using System.Text;
using Dapr.Client;
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

      var tokenDescripter = new SecurityTokenDescriptor()
      {
        Subject = new ClaimsIdentity(
        [
          new Claim(JwtRegisteredClaimNames.Sub, Convert.ToString(login.Id)),
          new Claim(JwtRegisteredClaimNames.Sub, login.Email)
        ]),
        Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
        SigningCredentials = credentials,
        Issuer = configuration["Jwt:Issuer"],
        Audience = configuration["Jwt:Audience"]
      };

      var handler = new JsonWebTokenHandler();
      string token = handler.CreateToken(tokenDescripter);

      return token;
    }
  }
}
