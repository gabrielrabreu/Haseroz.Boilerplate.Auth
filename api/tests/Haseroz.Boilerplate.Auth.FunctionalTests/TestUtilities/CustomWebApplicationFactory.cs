using Haseroz.Boilerplate.Auth.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace Haseroz.Boilerplate.Auth.FunctionalTests.TestUtilities;

public class CustomWebApplicationFactory : WebApplicationFactory<IWebMarker>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment("Development");
        var host = builder.Build();
        host.Start();
        return host;
    }
}
