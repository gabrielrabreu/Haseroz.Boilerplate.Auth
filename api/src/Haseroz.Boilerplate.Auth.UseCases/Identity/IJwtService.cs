using Haseroz.Boilerplate.Auth.UseCases.DataTransferObjects;

namespace Haseroz.Boilerplate.Auth.UseCases.Identity;

public interface IJwtService
{
    Task<TokenDto> CreateToken(ApplicationUser user);
}
