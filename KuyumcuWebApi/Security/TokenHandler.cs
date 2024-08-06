using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KuyumcuWebApi.Models;
using Microsoft.IdentityModel.Tokens;

namespace KuyumcuWebApi.Security;

public class TokenHandler
{

    public static Token CreateToken(IConfiguration configuration, User user)
    {
        Token token = new Token();
        var claims = new List<Claim>
        {
            new Claim("id",user.Id.ToString()),
            new Claim("email",user.Email),
            new Claim("firstName",user.FirstName),
            new Claim("lastName",user.LastName),
            new Claim("role",user.role.Name),
            new Claim(ClaimTypes.Name, user.FirstName),
            new Claim(ClaimTypes.Role,user.role.Name)
        };
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"])
        );
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        token.Expiration = DateTime.Now.AddMinutes(Convert.ToInt16(configuration["Token:Expiration"]));

        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
            issuer: "ab",
            audience: "ab",
            expires: token.Expiration,
            notBefore: DateTime.Now,
            claims: claims,
            signingCredentials: credentials
        );

        JwtSecurityTokenHandler tokenHandler = new();
        token.AccessToken = tokenHandler.WriteToken(jwtSecurityToken);

        return token;
    }
}