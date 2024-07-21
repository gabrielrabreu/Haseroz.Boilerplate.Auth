using Haseroz.Boilerplate.Auth.UseCases.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Haseroz.Boilerplate.Auth.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>;
