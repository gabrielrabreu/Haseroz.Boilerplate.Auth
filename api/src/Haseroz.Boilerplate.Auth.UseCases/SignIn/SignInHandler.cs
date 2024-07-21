using Ardalis.Result;
using Haseroz.Boilerplate.Auth.UseCases.DataTransferObjects;
using Haseroz.Boilerplate.Auth.UseCases.Identity;
using Haseroz.DevKit;
using Microsoft.AspNetCore.Identity;

namespace Haseroz.Boilerplate.Auth.UseCases.SignIn;

public class SignInHandler(UserManager<ApplicationUser> userManager, IJwtService jwtService) : ICommandHandler<SignInCommand, Result<TokenDto>>
{
    public async Task<Result<TokenDto>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByNameAsync(request.Username);

        if (user == null || !await userManager.CheckPasswordAsync(user, request.Password))
            return Result.Unauthorized();

        return await jwtService.CreateToken(user);
    }
}