using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Flooq.Api.Models;
using IdentityModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NuGet.Protocol;
using FlooqWebApplicationFactory = Flooq.IntegrationTest.FlooqWebApplicationFactory<Program>;

namespace Flooq.IntegrationTest;

[TestClass]
public class TokenTest
{
  private readonly HttpClient _client = FlooqWebApplicationFactory.Factory.CreateClient();

  [TestInitialize]
  public void Setup()
  {
    _client.DefaultRequestHeaders.Authorization 
      = new AuthenticationHeaderValue("Bearer", MockJwtTokens.GenerateJwtToken(new List<Claim>()
      {
        new (JwtClaimTypes.Scope, "read"),
        new (JwtClaimTypes.Scope, "write"),
        new (JwtClaimTypes.Scope, "read_all"),
        new (ClaimTypes.NameIdentifier, FlooqWebApplicationFactory.TestUserId.ToString())
      }));
  }

  [TestMethod]
  public async Task CanGetTokenIdsAndNamesByUser()
  {
    var response = await _client.GetAsync("api/Token/user");
    response.EnsureSuccessStatusCode();
    
    var content = response.Content.ReadAsStringAsync().Result;
    var tokens = JsonConvert.DeserializeObject<IEnumerable<Dictionary<string, string>>>(content)!;
    
    Assert.IsFalse(tokens.ToImmutableList().IsEmpty);
  }

  [TestMethod]
  public async Task CanGetToken()
  {
    var response = await _client.GetAsync($"api/Token/{FlooqWebApplicationFactory.TestTokenId}");
    response.EnsureSuccessStatusCode();
    
    var content = response.Content.ReadAsStringAsync().Result;
    var token = JsonConvert.DeserializeObject<Token>(content)!;
    
    Assert.AreEqual(FlooqWebApplicationFactory.TestTokenId, token.Id);
  }

  [TestMethod]
  public async Task CannotGetNonExistingToken()
  {
    var response = await _client.GetAsync($"api/Token/{Guid.NewGuid()}");

    Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
  }

  [TestMethod]
  public async Task CanPostToken()
  {
    var token = new Token
    {
      Name = "Test Token #2",
      UserId = FlooqWebApplicationFactory.TestUserId,
      Value = "TestToken"
    };
    var now = DateTime.UtcNow;
    var content = new StringContent(token.ToJson(), Encoding.UTF8, "application/json");

    var response = await _client.PostAsync("api/Token", content);
    response.EnsureSuccessStatusCode();
    
    var receivedContent = response.Content.ReadAsStringAsync().Result;
    var receivedToken = JsonConvert.DeserializeObject<Token>(receivedContent)!;
    
    Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
    Assert.AreEqual(token.Name, receivedToken.Name);
    Assert.IsInstanceOfType(receivedToken.Id, typeof(Guid));
    Assert.AreNotEqual(Guid.Empty, receivedToken.Id);
    Assert.IsInstanceOfType(receivedToken.LastEdited, typeof(DateTime));
    Assert.AreEqual(now.Date, receivedToken.LastEdited!.Value.Date);
    Assert.IsTrue(receivedToken.LastEdited!.Value.Ticks > now.Ticks);
  }

  [TestMethod]
  public async Task CannotPostExistingToken()
  {
    var token = new Token
    {
      Id = FlooqWebApplicationFactory.TestTokenId,
      Name = "Test Token #2",
      UserId = FlooqWebApplicationFactory.TestUserId,
      Value = "TestToken"
    };
    var content = new StringContent(token.ToJson(), Encoding.UTF8, "application/json");

    var response = await _client.PostAsync("api/Token", content);
    
    Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode);
  }
  
  [TestMethod]
  public async Task CannotPostTokenWithSameUserAndName()
  {
    var token = new Token
    {
      Name = "Test Token #1",
      UserId = FlooqWebApplicationFactory.TestUserId,
      Value = "TestToken"
    };
    var content = new StringContent(token.ToJson(), Encoding.UTF8, "application/json");

    var response = await _client.PostAsync("api/Token", content);
    
    Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode);
  }

  [TestMethod]
  public async Task CanPutToken()
  {
    var token = new Token
    {
      Id = FlooqWebApplicationFactory.TestTokenId,
      Name = "Test Token #2",
      UserId = FlooqWebApplicationFactory.TestUserId,
      Value = "TestToken"
    };
    var now = DateTime.UtcNow;
    var content = new StringContent(token.ToJson(), Encoding.UTF8, "application/json");

    var response = await _client.PutAsync($"api/Token/{FlooqWebApplicationFactory.TestTokenId}", content);
    response.EnsureSuccessStatusCode();
    
    var receivedContent = response.Content.ReadAsStringAsync().Result;
    var receivedToken = JsonConvert.DeserializeObject<Token>(receivedContent)!;
    
    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    Assert.AreEqual(token.Name, receivedToken.Name);
    Assert.AreEqual(token.Id, receivedToken.Id);
    Assert.IsTrue(receivedToken.LastEdited!.Value.Ticks > now.Ticks);
  }

  [TestMethod]
  public async Task CannotPutNonExistingToken()
  {
    var id = Guid.NewGuid();
    var token = new Token
    {
      Id = id,
      Name = "Test Token #2",
      UserId = FlooqWebApplicationFactory.TestUserId,
      Value = "TestToken"
    };
    var content = new StringContent(token.ToJson(), Encoding.UTF8, "application/json");

    var response = await _client.PutAsync($"api/Token/{id}", content);
    
    Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
  }

  [TestMethod]
  public async Task CannotPutTokenWithWrongId()
  {
    var token = new Token
    {
      Id = Guid.NewGuid(),
      Name = "Test Token #2",
      UserId = FlooqWebApplicationFactory.TestUserId,
      LastEdited = DateTime.Now,
      Value = "TestToken"
    };
    var content = new StringContent(token.ToJson(), Encoding.UTF8, "application/json");

    var response = await _client.PutAsync($"api/Token/{FlooqWebApplicationFactory.TestTokenId}", content);
    
    Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
  }

  [TestMethod]
  public async Task CannotPutTokenWithWrongUserId()
  {
    var token = new Token
    {
      Id = FlooqWebApplicationFactory.TestTokenId,
      Name = "Test Token #2",
      UserId = Guid.NewGuid(),
      LastEdited = DateTime.Now,
      Value = "TestToken"
    };
    var content = new StringContent(token.ToJson(), Encoding.UTF8, "application/json");

    var response = await _client.PutAsync($"api/Token/{FlooqWebApplicationFactory.TestTokenId}", content);
    
    Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
  }

  [TestMethod]
  public async Task CanDeleteToken()
  {
    var response = await _client.DeleteAsync($"api/Token/{FlooqWebApplicationFactory.TestTokenId}");
    response.EnsureSuccessStatusCode();
    
    Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);

    response = await _client.GetAsync($"api/Token/{FlooqWebApplicationFactory.TestTokenId}");
    
    Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
  }

  [TestMethod]
  public async Task CanDeleteAllTokens()
  {
    var response = await _client.DeleteAsync($"api/Token/all");
    response.EnsureSuccessStatusCode();
    
    Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);

    response = await _client.GetAsync($"api/Token/{FlooqWebApplicationFactory.TestTokenId}");
    
    Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
  }

  [TestMethod]
  public async Task CannotDeleteNonExistingToken()
  {
    var response = await _client.DeleteAsync($"api/Token/{Guid.NewGuid()}");
    
    Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
  }
}