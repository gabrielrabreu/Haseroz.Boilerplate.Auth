using Haseroz.Boilerplate.Auth.UseCases.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Haseroz.Boilerplate.Auth.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var initialiser = scope.ServiceProvider.GetRequiredService<AppDbContextInitialiser>();
        await initialiser.InitialiseAsync();
        await initialiser.SeedAsync();
    }
}

public class AppDbContextInitialiser(
    IConfiguration configuration,
    AppDbContext context,
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager)
{
    public async Task InitialiseAsync()
    {
        try
        {
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            throw new DatabaseInitializationException("An error occurred while initialising the database. See inner exception for details.", ex);
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            throw new DatabaseInitializationException("An error occurred while seeding the database. See inner exception for details.", ex);
        }
    }

    private async Task TrySeedAsync()
    {
        await EnsureRolesExistsAsync();
        await EnsureAdminUserExistsAsync();
    }

    private async Task EnsureRolesExistsAsync()
    {
        var roles = ApplicationRole.GetAllRoles();
        foreach (var role in roles)
            await EnsureRoleExistsAsync(role);
    }

    private async Task EnsureRoleExistsAsync(string roleName)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            var role = new ApplicationRole(roleName);
            await roleManager.CreateAsync(role);
        }
    }

    private async Task EnsureAdminUserExistsAsync()
    {
        var adminUserName = configuration["Admin:UserName"]!;
        var adminPassword = configuration["Admin:Password"]!;

        var administrator = new ApplicationUser { UserName = adminUserName };

        if (userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await userManager.CreateAsync(administrator, adminPassword);
            await userManager.AddToRoleAsync(administrator, ApplicationRole.Administrator);
        }
    }
}

public class DatabaseInitializationException : Exception
{
    public DatabaseInitializationException()
    {
    }

    public DatabaseInitializationException(string message)
        : base(message)
    {
    }

    public DatabaseInitializationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}