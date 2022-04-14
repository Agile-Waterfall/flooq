using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Factory = Flooq.IntegrationTest.FlooqWebApplicationFactory<Program>;

namespace Flooq.IntegrationTest;

[TestClass]
public class VersionTest
{
  private HttpClient _client;
  [TestInitialize]
  public void Setup()
  {
    _client = Factory.Factory.CreateClient();
  }
  
  [TestMethod]
  public async Task CanGetVersion()
  {
    var response = await _client.GetAsync("api/version");

    response.EnsureSuccessStatusCode();
    var version = response.Content.ReadAsStringAsync().Result;
    
    Assert.IsTrue(version.Contains(Factory.TEST_VERSION));
  }
}