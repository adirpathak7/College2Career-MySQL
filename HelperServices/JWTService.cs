using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using College2Career.Models;
using Microsoft.IdentityModel.Tokens;

public interface IJWTService
{
    string generateToken(Users users);
    //string extractEmailFromToken(string token);
}

public class JWTService : IJWTService
{
    private readonly string secretKey;
    private readonly string issuer;
    private readonly string audience;
    private readonly int expires;

    public JWTService(IConfiguration configuration)
    {
        secretKey = configuration["Jwt:Key"];
        issuer = configuration["Jwt:Issuer"];
        audience = configuration["Jwt:Audience"];
        expires = int.Parse(configuration["Jwt:Expires"]);
    }

    public string generateToken(Users users)
    {
        var claims = new[]
                    {
                        new Claim("usersId", users.usersId.ToString()),
                        new Claim(ClaimTypes.Email, users.email),
                        new Claim(ClaimTypes.Role, users.roleId == 1 ? "student" : "company"),
                        //new Claim("profilePicture", "ht tps://res.cloudinary.com/druzdz5zn/image/upload/v1744715705/lhi4cgauyc4nqttymcu4.webp")
                    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddHours(expires),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}