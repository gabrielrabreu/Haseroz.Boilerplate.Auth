using FluentAssertions;
using Haseroz.Boilerplate.Auth.FunctionalTests.TestUtilities;
using Haseroz.Boilerplate.Auth.WebApi.Endpoints.Samples;
using Haseroz.TestKit.FluentAssertions;
using System.Net.Http.Json;

namespace Haseroz.Boilerplate.Auth.FunctionalTests.Endpoints.Samples;

public class AnonymousTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task ReturnsOkGivenNoAuthorizationDone()
    {
        var response = await _client.GetAsync("/Samples/Anonymous");
        response.Should().BeOk();

        var result = await response.Content.ReadFromJsonAsync<UserInfoDto>();
        result.Should().NotBeNull();
        result!.Username.Should().BeEmpty();
        result.Claims.Should().BeEmpty();
    }
}
