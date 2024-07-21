using Ardalis.Result;
using Haseroz.Boilerplate.Auth.UseCases.DataTransferObjects;
using Haseroz.Boilerplate.Auth.UseCases.Identity;
using Haseroz.DevKit;
using Microsoft.AspNetCore.Identity;

namespace Haseroz.Boilerplate.Auth.UseCases.SignUp;

public class SignUpHandler(UserManager<ApplicationUser> userManager, IJwtService jwtService) : ICommandHandler<SignUpCommand, Result<TokenDto>>
{
    public async Task<Result<TokenDto>> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            UserName = request.Username,
            Email = request.Email
        };

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return Result.Invalid(result.AsErrors());
        }

       return await jwtService.CreateToken(user);
    }
}

public static class IdentityResultExtensions
{
    public static List<ValidationError> AsErrors(this IdentityResult identityResult)
    {
        var resultErrors = new List<ValidationError>();

        foreach (var error in identityResult.Errors)
        {
            resultErrors.Add(new ValidationError()
            {
                ErrorCode = error.Code,
                ErrorMessage = error.Description,
                Severity = ValidationSeverity.Error
            });
        }

        return resultErrors;
    }
}