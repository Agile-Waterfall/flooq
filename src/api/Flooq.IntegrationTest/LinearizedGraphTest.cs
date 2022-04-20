using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Flooq.Api.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NuGet.Protocol;
using FlooqWebApplicationFactory = Flooq.IntegrationTest.FlooqWebApplicationFactory<Program>;

namespace Flooq.IntegrationTest;

[TestClass]
public class LinearizedGraphTest
{
  private HttpClient _client = FlooqWebApplicationFactory.Factory.CreateClient();

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
    var response = await _client.GetAsync($"api/LinearizedGraph/{FlooqWebApplicationFactory.TEST_GUID}");
    response.EnsureSuccessStatusCode();
    
    var content = response.Content.ReadAsStringAsync().Result;
    var graph = JsonConvert.DeserializeObject<LinearizedGraph>(content)!;

    Assert.AreEqual(FlooqWebApplicationFactory.TEST_GUID, graph.Id);
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
      Graph = "Test"
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
  public async Task CannotPostExistingDataFlow()
  {
    var graph = new LinearizedGraph
    {
      Id = FlooqWebApplicationFactory.TEST_GUID,
      Graph = "Test"
    };

    var content = new StringContent(graph.ToJson(), Encoding.UTF8, "application/json");
    var response = await _client.PostAsync("api/LinearizedGraph", content);

    Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
  }
}