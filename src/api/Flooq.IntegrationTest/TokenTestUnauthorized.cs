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
  private HttpClient _client;

  [TestInitialize]
  public void Setup()
  {
    _client = FlooqWebApplicationFactory.Factory.CreateClient();
  }

  [TestMethod]
  public async Task CannotGetTokenNamesByUserUnauthorized()
  {
    var response = await _client.GetAsync("api/Token/user");
    
    Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
  }

  [TestMethod]
  public async Task CannotGetTokenUnauthorized()
  {
    var response = await _client.GetAsync($"api/Token/{FlooqWebApplicationFactory.TEST_TOKEN_ID}");
    
    Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
  }

  [TestMethod]
  public async Task CannotPostTokenUnauthorized()
  {
    var token = new Token
    {
      Name = "Test Token #2",
      UserId = FlooqWebApplicationFactory.TEST_USER_ID,
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
      Id = FlooqWebApplicationFactory.TEST_TOKEN_ID,
      Name = "Test Token #2",
      UserId = FlooqWebApplicationFactory.TEST_USER_ID,
      Value = "TestToken"
    };
    var content = new StringContent(token.ToJson(), Encoding.UTF8, "application/json");

    var response = await _client.PutAsync($"api/Token/{FlooqWebApplicationFactory.TEST_TOKEN_ID}", content);
    
    Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
  }

  [TestMethod]
  public async Task CannotDeleteTokenUnauthorized()
  {
    var response = await _client.DeleteAsync($"api/Token/{FlooqWebApplicationFactory.TEST_TOKEN_ID}");
    
    Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
  }
}