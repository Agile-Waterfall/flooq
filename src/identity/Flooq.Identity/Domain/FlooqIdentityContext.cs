using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Flooq.Identity.Models;

namespace Flooq.Identity.Data
{
  public class FlooqIdentityContext : IdentityDbContext<ApplicationUser>
  {
    public FlooqIdentityContext(DbContextOptions<FlooqIdentityContext> options)
        : base(options)
    {
    }
  }
}