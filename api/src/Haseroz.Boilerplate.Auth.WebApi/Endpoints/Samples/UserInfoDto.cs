namespace Haseroz.Boilerplate.Auth.WebApi.Endpoints.Samples;

public record UserInfoDto(string Username, IEnumerable<UserInfoClaimDto> Claims);

public record UserInfoClaimDto(string Type, string Value);
