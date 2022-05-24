using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlooqWebApplicationFactory = Flooq.IntegrationTest.FlooqWebApplicationFactory<Program>;

namespace Flooq.IntegrationTest;

[TestClass]
public class ContactTestUnauthorized
{
  private readonly HttpClient _client = FlooqWebApplicationFactory.Factory.CreateClient();

  [TestMethod]
  public async Task CannotGetContactsUnauthorized()
  {
    var response = await _client.GetAsync("api/Contact");
    
    Assert.AreEqual(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
  }
}