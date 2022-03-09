using Microsoft.EntityFrameworkCore;

namespace Flooq.Domain
{
  public class FlooqContext : DbContext
  {
    public FlooqContext(DbContextOptions<FlooqContext> options) : base(options)
    { }

    public DbSet<Flooq.Model.DataFlow>? DataFlows { get; set; }
    public DbSet<Flooq.Model.Version>? Versions { get; set; }
  }
}