using Microsoft.VisualStudio.TestTools.UnitTesting;
using Factory = Flooq.IntegrationTest.FlooqWebApplicationFactory<Program>;

namespace Flooq.IntegrationTest;

[TestClass]
public class VersionTest
{
  [TestMethod]
  public void CanGetVersion()
  {
    _CanGetVersion();
  }

  private async void _CanGetVersion()
  {
    var response = await Factory.Client.GetAsync("api/version");

    response.EnsureSuccessStatusCode();
    Assert.AreEqual(Factory.TEST_VERSION, response.Content.ToString());
  }
}