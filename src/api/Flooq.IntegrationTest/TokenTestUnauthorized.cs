using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Flooq.Api.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet.Protocol;
using FlooqWebApplicationFactory = Flooq.IntegrationTest.FlooqWebApplicationFactory<Program>;

namespace Flooq.IntegrationTest;

[TestClass]
public class TokenTestUnauthorized
{
  private readonly HttpClient _client = FlooqWebApplicationFactory.Factory.CreateClient();

  [TestMethod]
  public async Task CannotGetTokenIdsAndNamesByUserUnauthorized()
  {
    var response = await _client.GetAsync("api/Token/user");
    
    Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
  }

  [TestMethod]
  public async Task CannotGetTokenUnauthorized()
  {
    var response = await _client.GetAsync($"api/Token/{FlooqWebApplicationFactory.TestTokenId}");
    
    Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
  }

  [TestMethod]
  public async Task CannotPostTokenUnauthorized()
  {
    var token = new Token
    {
      Name = "Test Token #2",
      UserId = FlooqWebApplicationFactory.TestUserId,
      Value = "TestToken"
    };
    var content = new StringContent(token.ToJson(), Encoding.UTF8, "application/json");

    var response = await _client.PostAsync("api/Token", content);
    
    Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
  }

  [TestMethod]
  public async Task CannotPutTokenUnauthorized()
  {
    var token = new Token
    {
      Id = FlooqWebApplicationFactory.TestTokenId,
      Name = "Test Token #2",
      UserId = FlooqWebApplicationFactory.TestUserId,
      Value = "TestToken"
    };
    var content = new StringContent(token.ToJson(), Encoding.UTF8, "application/json");

    var response = await _client.PutAsync($"api/Token/{FlooqWebApplicationFactory.TestTokenId}", content);
    
    Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
  }

  [TestMethod]
  public async Task CannotDeleteTokenUnauthorized()
  {
    var response = await _client.DeleteAsync($"api/Token/{FlooqWebApplicationFactory.TestTokenId}");
    
    Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
  }
}