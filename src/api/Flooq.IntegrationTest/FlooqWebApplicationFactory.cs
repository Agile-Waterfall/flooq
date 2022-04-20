using System;
using System.Linq;
using Flooq.Api.Domain;
using Flooq.Api.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Version = Flooq.Api.Models.Version;

namespace Flooq.IntegrationTest;

/// <summary>
/// <c>WebApplicationFactory</c> for Flooq API application providing specific
/// service configuration for Flooq integration tests.
/// </summary>
/// <typeparam name="Program"></typeparam>
public class FlooqWebApplicationFactory<Program> : WebApplicationFactory<Program> where Program: class
{
  public static readonly Guid TEST_GUID = Guid.NewGuid();
  public static readonly string TEST_VERSION = "0.0.1";

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
          Tag = TEST_VERSION
        });
        
        db.DataFlows.Add(new DataFlow
        {
          Id = TEST_GUID,
          Name = "Demo Flow #2",
          Status = "Active",
          LastEdited = DateTime.Now,
          Definition = "{}"
        });
        
        db.Graphs.Add(new LinearizedGraph
        {
          Id = TEST_GUID,
          Graph = "{}"
        });
        
        db.SaveChanges();
      }
    });
  }
}