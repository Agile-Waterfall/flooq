using System;
using System.Linq;
using Flooq.Api.Domain;
using Flooq.Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Version = Flooq.Api.Models.Version;

namespace Flooq.IntegrationTest;

/// <summary>
/// <see cref="WebApplicationFactory{Program}"/> for Flooq API application providing specific
/// service configuration for Flooq integration tests.
/// </summary>
/// <typeparam name="Program"></typeparam>
public class FlooqWebApplicationFactory<Program> : WebApplicationFactory<Program> where Program: class
{
  public static readonly Guid TestGuidDataFlow = Guid.NewGuid();
  public static readonly Guid TestGuidGraph = Guid.NewGuid();
  public static readonly string TestVersion = "0.0.1";
  public static readonly Guid TestUserId = Guid.NewGuid();
  public static readonly Guid TestTokenId = Guid.NewGuid();
  public static readonly string TestEmail = "example@flooq.io";

  private static readonly FlooqWebApplicationFactory<Program> _factory = new();
  public static FlooqWebApplicationFactory<Program> Factory => _factory;

  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureServices(services =>
    {
      var descriptor = services.SingleOrDefault(
        d => d.ServiceType == typeof(DbContextOptions<FlooqContext>))!;

      services.Remove(descriptor);

      services.AddDbContext<FlooqContext>(options =>
      {
        options.UseInMemoryDatabase("FlooqContext");
      });

      services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
      {
        var config = new OpenIdConnectConfiguration
        {
          Issuer = MockJwtTokens.Issuer
        };
        
        config.SigningKeys.Add(MockJwtTokens.SecurityKey);
        options.Configuration = config;
      });
      
      services.AddAuthorization(options =>
      {
        options.AddPolicy("read", policy =>
        {
          policy.RequireAuthenticatedUser();
          policy.RequireClaim("scope", "read");
        });

        options.AddPolicy("write", policy =>
        {
          policy.RequireAuthenticatedUser();
          policy.RequireClaim("scope", "write");
        });
      });

      var sp = services.BuildServiceProvider();
      
      using (var scope = sp.CreateScope())
      {
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<FlooqContext>();
        db.Database.EnsureCreated();

        // Insert test data
        db.Versions.Add(new Version
        {
          Name = "TestVersion", 
          Notes = "IntegrationTest", 
          Tag = TestVersion
        });
        
        db.DataFlows.Add(new DataFlow
        {
          Id = TestGuidDataFlow,
          Name = "Demo Flow #2",
          Status = "Active",
          LastEdited = DateTime.Now,
          Definition = "{}",
          UserId = TestUserId
        });
        
        db.Graphs.Add(new LinearizedGraph
        {
          Id = TestGuidGraph,
          Graph = "{}"
        });

        db.Tokens.Add(new Token
        {
          Id = TestTokenId,
          Name = "Test Token #1",
          UserId = TestUserId,
          LastEdited = DateTime.Now,
          Value = "TestToken"
        });

        db.Contacts.Add(new Contact(TestEmail));
        
        db.SaveChanges();
      }
    });
  }
}