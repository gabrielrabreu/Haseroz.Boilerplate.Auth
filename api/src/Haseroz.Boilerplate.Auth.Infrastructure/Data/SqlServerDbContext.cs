using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Haseroz.Boilerplate.Auth.Infrastructure.Data;

public class SqlServerDbContext(IConfiguration configuration) : AppDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }
};
