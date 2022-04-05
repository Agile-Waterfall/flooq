using Flooq.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Flooq.Api.Domain
{
  public class FlooqContext : DbContext
  {
    public FlooqContext(DbContextOptions<FlooqContext> options) : base(options)
    { }

    public DbSet<DataFlow>? DataFlows { get; set; }
    public DbSet<Models.Version>? Versions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      string sampleData;
      using (StreamReader reader = new StreamReader("sampleDataFlow.json"))
      {
        sampleData = reader.ReadToEnd();
      }
      modelBuilder.Entity<DataFlow>().HasData(
        new DataFlow { Id = Guid.NewGuid(), Name = "Demo Flow #1", Status = "Active", LastEdited = new DateTime(2022, 3, 5, 14, 45, 12).ToUniversalTime(), Definition = sampleData});
    }
  }
}