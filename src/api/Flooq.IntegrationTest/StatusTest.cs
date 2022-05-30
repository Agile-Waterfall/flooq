using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlooqWebApplicationFactory = Flooq.IntegrationTest.FlooqWebApplicationFactory<Program>;

namespace Flooq.IntegrationTest;

[TestClass]
public class StatusTest
{
  private readonly HttpClient _client = FlooqWebApplicationFactory.Factory.CreateClient();

  [TestMethod]
  public async Task CanGetStatus()
  {
    var response = await _client.GetAsync("api/status");
    response.EnsureSuccessStatusCode();
    
    var status = response.Content.ReadAsStringAsync().Result;

    Assert.IsTrue(status.Contains("running"));
  }
}