using Bogus;
using FluentAssertions;
using Haseroz.Boilerplate.Auth.FunctionalTests.TestUtilities;
using Haseroz.Boilerplate.Auth.UseCases.DataTransferObjects;
using Haseroz.Boilerplate.Auth.WebApi.Endpoints.Accounts;
using Haseroz.TestKit.FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace Haseroz.Boilerplate.Auth.FunctionalTests.Endpoints.Accounts;

public class SignInTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client = factory.CreateClient();
    private readonly Faker _faker = new();

    [Fact]
    public async Task ReturnsUnauthorizedGivenInvalidCredentials()
    {
        var request = new SignInRequest
        {
            Username = _faker.Internet.UserName(),
            Password = _faker.Internet.PasswordCustom(),
        };

        var httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("/Accounts/SignIn", httpContent);
        response.Should().BeUnauthorized();
    }

    [Fact]
    public async Task ReturnsOkGivenValidCredentials()
    {
        var username = _faker.Internet.UserName();
        var email = _faker.Internet.Email();
        var password = _faker.Internet.PasswordCustom();

        await EnsureUserExists(username, email, password);

        var request = new SignInRequest
        {
            Username = username,
            Password = password,
        };

        var httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("/Accounts/SignIn", httpContent);
        response.Should().BeOk();

        var content = await response.Content.ReadFromJsonAsync<TokenDto>();
        content.Should().NotBeNull();
        content!.AccessToken.Should().NotBeEmpty();
        content.Username.Should().Be(username);
        content.Email.Should().Be(email);
    }

    private async Task EnsureUserExists(string username, string email, string password)
    {
        var request = new SignUpRequest
        {
            Username = username,
            Email = email,
            Password = password,
        };

        var httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("/Accounts/SignUp", httpContent);
        response.Should().BeOk();
    }
}
