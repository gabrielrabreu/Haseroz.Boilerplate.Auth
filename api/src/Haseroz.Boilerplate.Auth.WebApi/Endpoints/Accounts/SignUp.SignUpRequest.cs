namespace Haseroz.Boilerplate.Auth.WebApi.Endpoints.Accounts;

public class SignUpRequest
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
