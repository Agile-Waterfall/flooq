using Microsoft.EntityFrameworkCore;

namespace Flooq.Api.Domain
{
  public class FlooqContext : DbContext
  {
    public FlooqContext(DbContextOptions<FlooqContext> options) : base(options)
    { }

    public DbSet<Models.DataFlow>? DataFlows { get; set; }
    public DbSet<Models.Version>? Versions { get; set; }
  }
}