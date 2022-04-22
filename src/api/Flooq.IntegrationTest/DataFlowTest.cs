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
public class DataFlowTest
{
  private HttpClient _client;
  
  [TestInitialize]
  public void Setup()
  {
    _client = FlooqWebApplicationFactory.Factory.CreateClient();
  }
  
  [TestMethod]
  public async Task CanGetDataFlows()
  {
    var response = await _client.GetAsync("api/DataFlow");
    response.EnsureSuccessStatusCode();
    
    var content = response.Content.ReadAsStringAsync().Result;
    var dataFlows = JsonConvert.DeserializeObject<IEnumerable<DataFlow>>(content)!;
    
    Assert.IsFalse(dataFlows.ToImmutableList().IsEmpty);
  }

  [TestMethod]
  public async Task CanGetDataFlow()
  {
    var response = await _client.GetAsync($"api/DataFlow/{FlooqWebApplicationFactory.TEST_GUID_DATA_FLOW}");
    response.EnsureSuccessStatusCode();
    
    var content = response.Content.ReadAsStringAsync().Result;
    var dataFlow = JsonConvert.DeserializeObject<DataFlow>(content)!;
    
    Assert.AreEqual(FlooqWebApplicationFactory.TEST_GUID_DATA_FLOW, dataFlow.Id);
  }

  [TestMethod]
  public async Task CannotGetNonExistingDataFlow()
  {
    var response = await _client.GetAsync($"api/DataFlow/{Guid.NewGuid()}");

    Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
  }

  [TestMethod]
  public async Task CanPostDataFlow()
  {
    var dataFlow = new DataFlow()
    {
      Name = "Demo Flow #3",
      Status = "Active",
      Definition = "{}"
    };
    var now = DateTime.UtcNow;
    var content = new StringContent(dataFlow.ToJson(), Encoding.UTF8, "application/json");

    var response = await _client.PostAsync("api/DataFlow", content);
    response.EnsureSuccessStatusCode();
    
    var receivedContent = response.Content.ReadAsStringAsync().Result;
    var receivedDataFlow = JsonConvert.DeserializeObject<DataFlow>(receivedContent)!;
    
    Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
    Assert.AreEqual(dataFlow.Name, receivedDataFlow.Name);
    Assert.IsInstanceOfType(receivedDataFlow.Id, typeof(Guid));
    Assert.AreNotEqual(Guid.Empty, receivedDataFlow.Id);
    Assert.IsInstanceOfType(receivedDataFlow.LastEdited, typeof(DateTime));
    Assert.AreEqual(now.Date, receivedDataFlow.LastEdited!.Value.Date);
    Assert.IsTrue(receivedDataFlow.LastEdited!.Value.Ticks > now.Ticks);
  }

  [TestMethod]
  public async Task CannotPostExistingDataFlow()
  {
    var dataFlow = new DataFlow()
    {
      Id = FlooqWebApplicationFactory.TEST_GUID_DATA_FLOW,
      Name = "Demo Flow #3",
      Status = "Active",
      Definition = "{}"
    };
    var content = new StringContent(dataFlow.ToJson(), Encoding.UTF8, "application/json");

    var response = await _client.PostAsync("api/DataFlow", content);
    
    Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
  }

  [TestMethod]
  public async Task CanPutDataFlow()
  {
    var dataFlow = new DataFlow()
    {
      Id = FlooqWebApplicationFactory.TEST_GUID_DATA_FLOW,
      Name = "Demo Flow #3",
      Status = "Active",
      Definition = "{}"
    };
    var now = DateTime.UtcNow;
    var content = new StringContent(dataFlow.ToJson(), Encoding.UTF8, "application/json");

    var response = await _client.PutAsync($"api/DataFlow/{FlooqWebApplicationFactory.TEST_GUID_DATA_FLOW}", content);
    response.EnsureSuccessStatusCode();
    
    var receivedContent = response.Content.ReadAsStringAsync().Result;
    var receivedDataFlow = JsonConvert.DeserializeObject<DataFlow>(receivedContent)!;
    
    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    Assert.AreEqual(dataFlow.Name, receivedDataFlow.Name);
    Assert.AreEqual(dataFlow.Id, receivedDataFlow.Id);
    Assert.IsTrue(receivedDataFlow.LastEdited!.Value.Ticks > now.Ticks);
  }

  [TestMethod]
  public async Task CannotPutNonExistingDataFlow()
  {
    var id = Guid.NewGuid();
    var dataFlow = new DataFlow()
    {
      Id = id,
      Name = "Demo Flow #3",
      Status = "Active",
      Definition = "{}"
    };
    var content = new StringContent(dataFlow.ToJson(), Encoding.UTF8, "application/json");

    var response = await _client.PutAsync($"api/DataFlow/{id}", content);
    
    Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
  }

  [TestMethod]
  public async Task CannotPutDataFlowWithWrongId()
  {
    var dataFlow = new DataFlow()
    {
      Id = Guid.NewGuid(),
      Name = "Demo Flow #3",
      Status = "Active",
      LastEdited = DateTime.Now,
      Definition = "{}"
    };
    var content = new StringContent(dataFlow.ToJson(), Encoding.UTF8, "application/json");

    var response = await _client.PutAsync($"api/DataFlow/{FlooqWebApplicationFactory.TEST_GUID_DATA_FLOW}", content);
    
    Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
  }

  [TestMethod]
  public async Task CanDeleteDataFlow()
  {
    var response = await _client.DeleteAsync($"api/DataFlow/{FlooqWebApplicationFactory.TEST_GUID_DATA_FLOW}");
    response.EnsureSuccessStatusCode();
    
    Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);

    response = await _client.GetAsync($"api/DataFlow/{FlooqWebApplicationFactory.TEST_GUID_DATA_FLOW}");
    
    Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
  }

  [TestMethod]
  public async Task CannotDeleteNonExistingDataFlow()
  {
    var response = await _client.DeleteAsync($"api/DataFlow/{Guid.NewGuid()}");
    
    Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
  }
}