using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Flooq.IntegrationTest;

[TestClass]
public class StatusTest
{
  private FlooqWebApplicationFactory<Program> _factory;

  [TestInitialize]
  public void Setup()
  {
    _factory = new FlooqWebApplicationFactory<Program>();
  }

  [TestMethod]
  public void CanGetStatus()
  {
    _CanGetStatus();
  }

  private async void _CanGetStatus()
  {
    var client = _factory.CreateClient();

    var response = await client.GetAsync("api/status");

    response.EnsureSuccessStatusCode();
    Assert.AreEqual("running", response.Content.ToString());
  }
}