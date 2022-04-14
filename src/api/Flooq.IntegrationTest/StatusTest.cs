using Microsoft.VisualStudio.TestTools.UnitTesting;
using Factory = Flooq.IntegrationTest.FlooqWebApplicationFactory<Program>;

namespace Flooq.IntegrationTest;

[TestClass]
public class StatusTest
{
  [TestMethod]
  public void CanGetStatus()
  {
    _CanGetStatus();
  }

  private async void _CanGetStatus()
  {
    var response = await Factory.Client.GetAsync("api/status");

    response.EnsureSuccessStatusCode();
    Assert.AreEqual("running", response.Content.ToString());
  }
}