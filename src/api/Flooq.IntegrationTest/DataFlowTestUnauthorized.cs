using System;
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
public class DataFlowTestUnauthorized
{
  private readonly HttpClient _client = FlooqWebApplicationFactory.Factory.CreateClient();

  [TestMethod]
  public async Task CannotGetDataFlowsByUserUnauthorized()
  {
    var response = await _client.GetAsync("api/DataFlow/user");
    
    Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
  }

  [TestMethod]
  public async Task CannotGetDataFlowUnauthorized()
  {
    var response = await _client.GetAsync($"api/DataFlow/{FlooqWebApplicationFactory.TestGuidDataFlow}");
    
    Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
  }

  [TestMethod]
  public async Task CannotGetDataFlowByUserUnauthorized()
  {
    var response = await _client.GetAsync($"api/DataFlow/user/{FlooqWebApplicationFactory.TestGuidDataFlow}");
    
    Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
  }

  [TestMethod]
  public async Task CannotPostDataFlowUnauthorized()
  {
    var dataFlow = new DataFlow()
    {
      Name = "Demo Flow #3",
      Status = "Active",
      Definition = "{}",
      UserId = FlooqWebApplicationFactory.TestUserId
    };
    var now = DateTime.UtcNow;
    var content = new StringContent(dataFlow.ToJson(), Encoding.UTF8, "application/json");

    var response = await _client.PostAsync("api/DataFlow", content);
    
    Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
  }

  [TestMethod]
  public async Task CannotPutDataFlowUnauthorized()
  {
    var dataFlow = new DataFlow()
    {
      Id = FlooqWebApplicationFactory.TestGuidDataFlow,
      Name = "Demo Flow #3",
      Status = "Active",
      Definition = "{}",
      UserId = FlooqWebApplicationFactory.TestUserId
    };
    var now = DateTime.UtcNow;
    var content = new StringContent(dataFlow.ToJson(), Encoding.UTF8, "application/json");

    var response = await _client.PutAsync($"api/DataFlow/{FlooqWebApplicationFactory.TestGuidDataFlow}", content);
    
    Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
  }

  [TestMethod]
  public async Task CannotDeleteDataFlowUnauthorized()
  {
    var response = await _client.DeleteAsync($"api/DataFlow/{FlooqWebApplicationFactory.TestGuidDataFlow}");
    
    Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
  }
}