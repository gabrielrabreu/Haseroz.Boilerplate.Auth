using Haseroz.Boilerplate.Auth.UseCases.DataTransferObjects;
using Haseroz.Boilerplate.Auth.UseCases.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Haseroz.Boilerplate.Auth.Infrastructure.Identity;

public class JwtService(UserManager<ApplicationUser> userManager, IOptions<JwtSettings> jwtSettings) : IJwtService
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public async Task<TokenDto> CreateToken(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName!)
        };

        var roles = await userManager.GetRolesAsync(user);

        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var accessToken = CreateAccessToken(claims);

        return new TokenDto(new JwtSecurityTokenHandler().WriteToken(accessToken), user.UserName!, user.Email!);
    }

    private JwtSecurityToken CreateAccessToken(IList<Claim> claims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        return new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenValidityInMinutes),
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
    }
}
