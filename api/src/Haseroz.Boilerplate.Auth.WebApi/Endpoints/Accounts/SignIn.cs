using Haseroz.Boilerplate.Auth.UseCases.DataTransferObjects;
using Haseroz.Boilerplate.Auth.UseCases.SignIn;
using Haseroz.DevKit.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Haseroz.Boilerplate.Auth.WebApi.Endpoints.Accounts;

public class SignIn : MinimalApiEndpoint
{
    public override void Define(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/Accounts/SignIn", HandleAsync)
            .WithOpenApi()
            .WithTags("Accounts")
            .ProducesOk<TokenDto>()
            .ProducesDefaultErrorResponses();
    }

    private async Task<IResult> HandleAsync(
        [FromBody] SignInRequest request,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new SignInCommand(request.Username, request.Password);
        var result = await mediator.Send(command, cancellationToken);
        return result.ToMinimalApiResult();
    }
}
