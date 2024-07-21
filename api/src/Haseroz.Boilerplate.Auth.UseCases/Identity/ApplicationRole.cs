using Microsoft.AspNetCore.Identity;

namespace Haseroz.Boilerplate.Auth.UseCases.Identity;

public class ApplicationRole : IdentityRole<Guid>
{
    public const string Administrator = nameof(Administrator);
    public const string User = nameof(User);

    public ApplicationRole()
    {
    }

    public ApplicationRole(string roleName) : base(roleName)
    {
    }

    public static List<string> GetAllRoles()
    {
        return [Administrator, User];
    }
};
