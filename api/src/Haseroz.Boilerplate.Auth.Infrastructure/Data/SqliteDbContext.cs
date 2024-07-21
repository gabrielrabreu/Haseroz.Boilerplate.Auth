using Microsoft.EntityFrameworkCore;

namespace Haseroz.Boilerplate.Auth.Infrastructure.Data;

public class SqliteDbContext : AppDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=db.sqlite");
    }
};