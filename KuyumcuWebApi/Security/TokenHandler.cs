using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace KuyumcuWebApi.Security;

public class TokenHandler {

    public static Token CreateToken(IConfiguration configuration) {
        Token token= new Token();
        var claims = new List<Claim>
        {
            new Claim("name", "ax"),
            new Claim(ClaimTypes.Name, "emre"),
            new Claim("CustomClaim", "CustomValue")
        };
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"])
        );
        SigningCredentials credentials= new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

        token.Expiration=DateTime.Now.AddMinutes(Convert.ToInt16(configuration["Token:Expiration"]));

        JwtSecurityToken jwtSecurityToken= new JwtSecurityToken(
            issuer : "ab",
            audience : "ab",
            expires : token.Expiration,
            notBefore : DateTime.Now,
            claims : claims,
            signingCredentials : credentials
        );

        JwtSecurityTokenHandler tokenHandler= new();
        token.AccessToken = tokenHandler.WriteToken(jwtSecurityToken);

        return token;
    }
}