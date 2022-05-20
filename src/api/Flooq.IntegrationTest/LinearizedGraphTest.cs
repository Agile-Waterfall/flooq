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
public class LinearizedGraphTest
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
        new (ClaimTypes.NameIdentifier, FlooqWebApplicationFactory.TEST_USER_ID.ToString())
      }));
  }

  [TestMethod]
  public async Task CanGetLinearizedGraphs()
  {
    var response = await _client.GetAsync("api/LinearizedGraph");
    response.EnsureSuccessStatusCode();

    var content = response.Content.ReadAsStringAsync().Result;
    var graphs = JsonConvert.DeserializeObject<IEnumerable<LinearizedGraph>>(content)!;

    Assert.IsFalse(graphs.ToImmutableList().IsEmpty);
  }

  [TestMethod]
  public async Task CanGetLinearizedGraph()
  {
    var response = await _client.GetAsync($"api/LinearizedGraph/{FlooqWebApplicationFactory.TEST_GUID_GRAPH}");
    response.EnsureSuccessStatusCode();
    
    var content = response.Content.ReadAsStringAsync().Result;
    var graph = JsonConvert.DeserializeObject<LinearizedGraph>(content)!;

    Assert.AreEqual(FlooqWebApplicationFactory.TEST_GUID_GRAPH, graph.Id);
  }

  [TestMethod]
  public async Task CannotGetNonExistingLinearizedGraph()
  {
    var response = await _client.GetAsync($"api/LinearizedGraph/{Guid.NewGuid()}");

    Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
  }

  [TestMethod]
  public async Task CanPostLinearizedGraph()
  {
    var graph = new LinearizedGraph
    {
      Id = Guid.NewGuid(),
      Graph = "Test Graph #1"
    };

    var content = new StringContent(graph.ToJson(), Encoding.UTF8, "application/json");

    var response = await _client.PostAsync("api/LinearizedGraph", content);
    response.EnsureSuccessStatusCode();

    var receivedContent = response.Content.ReadAsStringAsync().Result;
    var receivedGraph = JsonConvert.DeserializeObject<LinearizedGraph>(receivedContent)!;

    Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
    Assert.IsInstanceOfType(receivedGraph.Id, typeof(Guid));
    Assert.AreEqual(graph.Id, receivedGraph.Id);
    Assert.IsInstanceOfType(receivedGraph.Graph, typeof(string));
    Assert.AreEqual(graph.Graph, receivedGraph.Graph);
  }

  [TestMethod]
  public async Task CannotPostExistingLinearizedGraph()
  {
    var graph = new LinearizedGraph
    {
      Id = FlooqWebApplicationFactory.TEST_GUID_GRAPH,
      Graph = "Test"
    };

    var content = new StringContent(graph.ToJson(), Encoding.UTF8, "application/json");
    var response = await _client.PostAsync("api/LinearizedGraph", content);

    Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode);
  }

  [TestMethod]
  public async Task DeletesExistingGraphOnPutDataFlow()
  {
    var id = Guid.NewGuid();
    var dataFlow = new DataFlow
    {
      Id = id,
      Name = "Demo Flow #1",
      Definition = "{}",
      UserId = FlooqWebApplicationFactory.TEST_USER_ID
    };

    // POST data flow
    var flowContentPost = new StringContent(dataFlow.ToJson(), Encoding.UTF8, "application/json");
    var flowResponsePost = await _client.PostAsync("api/DataFlow", flowContentPost);
    flowResponsePost.EnsureSuccessStatusCode();
    
    var graph = new LinearizedGraph
    {
      Id = id,
      Graph = "Test Graph #2"
    };
    
    // POST graph with same Id as the data flow above
    var graphContentPost = new StringContent(graph.ToJson(), Encoding.UTF8, "application/json");
    var graphResponsePost = await _client.PostAsync("api/LinearizedGraph", graphContentPost);
    graphResponsePost.EnsureSuccessStatusCode();
    
    var graphResponseGet = await _client.GetAsync($"api/LinearizedGraph/{id}");
    graphResponseGet.EnsureSuccessStatusCode();

    // PUT data flow
    var dataFlowPut = new DataFlow
    {
      Id = id,
      Name = "Demo Flow #1",
      Status = "Active",
      Definition = "{Changed}",
      UserId = FlooqWebApplicationFactory.TEST_USER_ID
    };
    var flowContentPut = new StringContent(dataFlowPut.ToJson(), Encoding.UTF8, "application/json");

    var flowResponsePut = await _client.PutAsync($"api/DataFlow/{id}", flowContentPut);
    flowResponsePut.EnsureSuccessStatusCode();
    
    // Check if linearized graph was successfully removed (with GET).
    graphResponseGet = await _client.GetAsync($"api/LinearizedGraph/{id}");
    Assert.AreEqual(HttpStatusCode.NotFound, graphResponseGet.StatusCode);
  }
}