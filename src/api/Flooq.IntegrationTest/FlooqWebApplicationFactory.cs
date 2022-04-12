using System.Linq;
using Flooq.Api.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Flooq.IntegrationTest;

public class FlooqWebApplicationFactory<Program> : WebApplicationFactory<Program> where Program: class
{
  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureServices(services =>
    {
      var descriptor = services.SingleOrDefault(
        d => d.ServiceType == typeof(DbContextOptions<FlooqContext>));

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
      }
    });
  }
}