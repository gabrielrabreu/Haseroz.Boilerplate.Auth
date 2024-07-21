namespace Haseroz.Boilerplate.Auth.Domain;

public static class Roles
{
    public const string Administrator = nameof(Administrator);
    public const string User = nameof(User);

    public static List<string> GetAllRoles()
    {
        return typeof(Roles)
            .GetFields()
            .Where(f => f.IsLiteral)
            .Select(f => f.GetValue(null)!.ToString()!)!
            .ToList();
    }
}