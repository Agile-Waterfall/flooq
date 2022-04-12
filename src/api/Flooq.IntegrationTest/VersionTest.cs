using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Flooq.IntegrationTest;

[TestClass]
public class VersionTest
{
  private FlooqWebApplicationFactory<Program> _factory;

  [TestInitialize]
  public void Setup()
  {
    _factory = new FlooqWebApplicationFactory<Program>();
  }

  [TestMethod]
  public void CanGetVersion()
  {
    _CanGetVersion();
  }

  private async void _CanGetVersion()
  {
    var client = _factory.CreateClient();

    var response = await client.GetAsync("api/version");

    response.EnsureSuccessStatusCode();
    Assert.AreEqual(FlooqWebApplicationFactory<Program>.TEST_VERSION, response.Content.ToString());
  }
}