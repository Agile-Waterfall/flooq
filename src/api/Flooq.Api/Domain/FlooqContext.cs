using Flooq.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Version = Flooq.Api.Models.Version;

namespace Flooq.Api.Domain
{
  #pragma warning disable CS1591
  public class FlooqContext : IdentityDbContext<IdentityUser>
  {
    public FlooqContext(DbContextOptions<FlooqContext> options) : base(options)
    { }
    public DbSet<DataFlow> DataFlows => Set<DataFlow>();
    public DbSet<Version> Versions => Set<Version>();
    public DbSet<LinearizedGraph> Graphs => Set<LinearizedGraph>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<DataFlow>()
        .Property(flow => flow.Id)
        .ValueGeneratedOnAdd();

      modelBuilder.Entity<DataFlow>()
        .Property(flow => flow.Status)
        .HasDefaultValue("Disabled");

      modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
      modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey();
      modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();

      modelBuilder.Entity<IdentityUser>().Ignore(c => c.ConcurrencyStamp);
      modelBuilder.Entity<IdentityUser>().Ignore(c => c.EmailConfirmed);
      modelBuilder.Entity<IdentityUser>().Ignore(c => c.LockoutEnabled);
      modelBuilder.Entity<IdentityUser>().Ignore(c => c.LockoutEnd);
      modelBuilder.Entity<IdentityUser>().Ignore(c => c.NormalizedEmail);
      modelBuilder.Entity<IdentityUser>().Ignore(c => c.PasswordHash);
      modelBuilder.Entity<IdentityUser>().Ignore(c => c.PhoneNumber);
      modelBuilder.Entity<IdentityUser>().Ignore(c => c.SecurityStamp);
      modelBuilder.Entity<IdentityUser>().Ignore(c => c.AccessFailedCount);
      modelBuilder.Entity<IdentityUser>().Ignore(c => c.NormalizedUserName);
      modelBuilder.Entity<IdentityUser>().Ignore(c => c.PhoneNumberConfirmed);
      modelBuilder.Entity<IdentityUser>().Ignore(c => c.TwoFactorEnabled);
    }
  }
  #pragma warning restore CS1591
}