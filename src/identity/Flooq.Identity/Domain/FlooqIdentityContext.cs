using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IdentityServerHost.Models;

namespace IdentityServerAspNetIdentity.Data
{
  public class FlooqIdentityContext : IdentityDbContext<ApplicationUser>
  {
    public FlooqIdentityContext(DbContextOptions<FlooqIdentityContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
    }
  }
}