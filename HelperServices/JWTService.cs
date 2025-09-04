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
        secretKey = Environment.GetEnvironmentVariable("JWT_SECRET");
        issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "C2COwner";
        audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "C2CUsers";
        expires = int.TryParse(Environment.GetEnvironmentVariable("JWT_EXPIRES"), out var e) ? e : 1;
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