using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Flooq.Authentication.Data;

public class FlooqAuthenticationContext : IdentityDbContext
{
  public FlooqAuthenticationContext(DbContextOptions<FlooqAuthenticationContext> options)
    : base(options)
  {
  }
}