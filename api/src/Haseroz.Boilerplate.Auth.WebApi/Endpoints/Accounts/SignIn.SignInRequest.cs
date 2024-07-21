namespace Haseroz.Boilerplate.Auth.WebApi.Endpoints.Accounts;

public class SignInRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
