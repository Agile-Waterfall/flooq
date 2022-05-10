using Flooq.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using Version = Flooq.Api.Models.Version;

namespace Flooq.Api.Domain
{
  #pragma warning disable CS1591
  public class FlooqContext : DbContext
  {
    private readonly IEncryptionProvider _provider;

    public FlooqContext(DbContextOptions<FlooqContext> options, IConfiguration config) : base(options)
    {
      var encryptionKey = config.GetValue<byte[]>("TOKEN_ENCRYPTION_KEY");
      var encryptionIv = config.GetValue<byte[]>("TOKEN_ENCRYPTION_IV");
      _provider = new AesProvider(encryptionKey, encryptionIv);
    }

    public DbSet<DataFlow> DataFlows => Set<DataFlow>();
    public DbSet<Version> Versions => Set<Version>();
    public DbSet<LinearizedGraph> Graphs => Set<LinearizedGraph>();
    public DbSet<Token> Tokens => Set<Token>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<DataFlow>()
        .Property(flow => flow.Id)
        .ValueGeneratedOnAdd();

      modelBuilder.Entity<DataFlow>()
        .Property(flow => flow.Status)
        .HasDefaultValue("Disabled");
      
      modelBuilder.UseEncryption(_provider);
    }
  }
  #pragma warning restore CS1591
}