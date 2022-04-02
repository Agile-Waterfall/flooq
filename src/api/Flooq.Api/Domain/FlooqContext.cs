using Microsoft.EntityFrameworkCore;

namespace Flooq.Api.Domain
{
  public class FlooqContext : DbContext
  {
    public FlooqContext(DbContextOptions<FlooqContext> options) : base(options)
    { }

    public DbSet<Model.DataFlow>? DataFlows { get; set; }
    public DbSet<Model.Version>? Versions { get; set; }
  }
}