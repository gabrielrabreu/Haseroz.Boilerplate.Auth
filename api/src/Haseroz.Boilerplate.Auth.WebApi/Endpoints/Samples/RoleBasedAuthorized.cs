using Haseroz.Boilerplate.Auth.UseCases.Identity;
using Haseroz.DevKit.AspNetCore;
using System.Security.Claims;

namespace Haseroz.Boilerplate.Auth.WebApi.Endpoints.Samples;

public class RoleBasedAuthorized : MinimalApiEndpoint
{
    public override void Define(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/Samples/RoleBasedAuthorized", Handle)
            .WithOpenApi()
            .WithTags("Samples")
            .ProducesOk<UserInfoDto>()
            .ProducesDefaultErrorResponses()
            .RequireAuthorization(builder => builder.RequireRole(ApplicationRole.Administrator));
    }

    private IResult Handle(ClaimsPrincipal user)
    {
        return TypedResults.Ok(new UserInfoDto(
            user.Identity?.Name ?? string.Empty,
            user.Claims.Select(c => new UserInfoClaimDto(c.Type, c.Value))));
    }
}