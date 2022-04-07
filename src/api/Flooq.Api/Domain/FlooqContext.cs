using Flooq.Api.Models;
using Microsoft.EntityFrameworkCore;
using Version = Flooq.Api.Models.Version;

namespace Flooq.Api.Domain
{
  public class FlooqContext : DbContext
  {
    public FlooqContext(DbContextOptions<FlooqContext> options) : base(options)
    { }
    public DbSet<DataFlow> DataFlows => Set<DataFlow>();
    public DbSet<Version> Versions => Set<Version>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      string sampleData;
      using (StreamReader reader = new StreamReader("sampleDataFlow.json"))
      {
        sampleData = reader.ReadToEnd();
      }
      modelBuilder.Entity<DataFlow>().HasData(
        new DataFlow { 
          Id = Guid.NewGuid(), 
          Name = "Demo Flow #1", 
          Status = "Active", 
          LastEdited = new DateTime(2022, 3, 5, 14, 45, 12).ToUniversalTime(), 
          Definition = sampleData
        });

      modelBuilder.Entity<DataFlow>()
        .Property(df => df.Id)
        .ValueGeneratedOnAdd();

      modelBuilder.Entity<DataFlow>()
        .Property(df => df.LastEdited)
        .HasDefaultValueSql("now()");
    }
  }
}