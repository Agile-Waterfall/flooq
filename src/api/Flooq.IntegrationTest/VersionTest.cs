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
  public void GetVersionTest()
  {
    var client = _factory.CreateClient();
    var response = client.GetAsync("/api/version");
    
    Assert.IsNotNull(response);
  }
}