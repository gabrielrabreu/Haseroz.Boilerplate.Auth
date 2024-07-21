using Haseroz.DevKit.AspNetCore;
using System.Security.Claims;

namespace Haseroz.Boilerplate.Auth.WebApi.Endpoints.Samples;

public class Authorized : MinimalApiEndpoint
{
    public override void Define(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/Samples/Authorized", Handle)
            .WithOpenApi()
            .WithTags("Samples")
            .ProducesOk<UserInfoDto>()
            .ProducesDefaultErrorResponses()
            .RequireAuthorization();
    }

    private IResult Handle(ClaimsPrincipal user)
    {
        return TypedResults.Ok(new UserInfoDto(
            user.Identity?.Name ?? string.Empty,
            user.Claims.Select(c => new UserInfoClaimDto(c.Type, c.Value))));
    }
}
