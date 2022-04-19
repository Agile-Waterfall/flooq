using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using FlooqWebApplicationFactory = Flooq.IntegrationTest.FlooqWebApplicationFactory<Program>;
using Version = Flooq.Api.Models.Version;

namespace Flooq.IntegrationTest;

[TestClass]
public class VersionTest
{
  private HttpClient _client;
  
  [TestInitialize]
  public void Setup()
  {
    _client = FlooqWebApplicationFactory.Factory.CreateClient();
  }
  
  [TestMethod]
  public async Task CanGetVersion()
  {
    var response = await _client.GetAsync("api/version");
    response.EnsureSuccessStatusCode();

    var content = response.Content.ReadAsStringAsync().Result;
    var version = JsonConvert.DeserializeObject<Version>(content)!;

    Assert.AreEqual(FlooqWebApplicationFactory.TEST_VERSION, version.Tag);
  }
}