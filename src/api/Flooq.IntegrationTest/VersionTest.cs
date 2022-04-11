using Microsoft.AspNetCore.Mvc.Testing;

namespace Flooq.IntegrationTest;

public class VersionTest
{
  public void GetVersionTest()
  {
    var application = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
    {
      
    });
  }
}