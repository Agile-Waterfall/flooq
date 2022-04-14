using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Factory = Flooq.IntegrationTest.FlooqWebApplicationFactory<Program>;

namespace Flooq.IntegrationTest;

[TestClass]
public class StatusTest
{
  private HttpClient _client;
  
  [TestInitialize]
  public void Setup()
  {
    _client = Factory.Factory.CreateClient();
  }
  
  [TestMethod]
  public async Task CanGetStatus()
  {
    var response = await _client.GetAsync("api/status");
    response.EnsureSuccessStatusCode();
    
    var status = response.Content.ReadAsStringAsync().Result;

    Assert.IsTrue(status.Contains("running"));
  }
}