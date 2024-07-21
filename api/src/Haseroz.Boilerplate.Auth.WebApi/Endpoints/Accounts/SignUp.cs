using Haseroz.Boilerplate.Auth.UseCases.DataTransferObjects;
using Haseroz.Boilerplate.Auth.UseCases.SignUp;
using Haseroz.DevKit.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Haseroz.Boilerplate.Auth.WebApi.Endpoints.Accounts;

public class SignUp : MinimalApiEndpoint
{
    public override void Define(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/Accounts/SignUp", HandleAsync)
            .WithOpenApi()
            .WithTags("Accounts")
            .ProducesOk<TokenDto>()
            .ProducesDefaultErrorResponses();
    }

    private async Task<IResult> HandleAsync(
        [FromBody] SignUpRequest request,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new SignUpCommand(request.Username, request.Email, request.Password);
        var result = await mediator.Send(command, cancellationToken);
        return result.ToMinimalApiResult();
    }
}
