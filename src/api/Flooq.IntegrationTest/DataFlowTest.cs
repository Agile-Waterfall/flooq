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
using Factory = Flooq.IntegrationTest.FlooqWebApplicationFactory<Program>;

namespace Flooq.IntegrationTest;

[TestClass]
public class DataFlowTest
{
  private HttpClient _client;
  
  [TestInitialize]
  public void Setup()
  {
    _client = Factory.Factory.CreateClient();
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
    var response = await _client.GetAsync($"api/DataFlow/{Factory.TEST_GUID}");
    response.EnsureSuccessStatusCode();
    
    var content = response.Content.ReadAsStringAsync().Result;
    var dataFlow = JsonConvert.DeserializeObject<DataFlow>(content)!;
    
    Assert.AreEqual(Factory.TEST_GUID, dataFlow.Id);
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
      Id = Guid.NewGuid(),
      Name = "Demo Flow",
      Status = "Active",
      LastEdited = DateTime.Now,
      Definition = "{}"
    };
    var content = new StringContent(dataFlow.ToJson(), Encoding.UTF8, "application/json");

    var response = await _client.PostAsync("api/DataFlow", content);
    response.EnsureSuccessStatusCode();
    
    Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
  }

  [TestMethod]
  public async Task CanPutDataFlow()
  {
    var dataFlow = new DataFlow()
    {
      Id = Factory.TEST_GUID,
      Name = "Demo Flow 2",
      Status = "Active",
      LastEdited = DateTime.Now,
      Definition = "{}"
    };
    var content = new StringContent(dataFlow.ToJson(), Encoding.UTF8, "application/json");

    var response = await _client.PutAsync($"api/DataFlow/{Factory.TEST_GUID}", content);
    response.EnsureSuccessStatusCode();
    
    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
  }

  [TestMethod]
  public async Task CannotPutNonExistingDataFlow()
  {
    var id = Guid.NewGuid();
    var dataFlow = new DataFlow()
    {
      Id = id,
      Name = "Demo Flow 2",
      Status = "Active",
      LastEdited = DateTime.Now,
      Definition = "{}"
    };
    var content = new StringContent(dataFlow.ToJson(), Encoding.UTF8, "application/json");

    var response = await _client.PutAsync($"api/DataFlow/{id}", content);
    
    Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
  }

  [TestMethod]
  public async Task CannotPutDataFlowWithWrongId()
  {
    var id = Guid.NewGuid();
    var dataFlow = new DataFlow()
    {
      Id = id,
      Name = "Demo Flow 2",
      Status = "Active",
      LastEdited = DateTime.Now,
      Definition = "{}"
    };
    var content = new StringContent(dataFlow.ToJson(), Encoding.UTF8, "application/json");

    var response = await _client.PutAsync($"api/DataFlow/{Factory.TEST_GUID}", content);
    
    Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
  }

  [TestMethod]
  public async Task CanDeleteDataFlow()
  {
    var response = await _client.DeleteAsync($"api/DataFlow/{Factory.TEST_GUID}");
    response.EnsureSuccessStatusCode();
    
    Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
  }

  [TestMethod]
  public async Task CannotDeleteNonExistingDataFlow()
  {
    var response = await _client.DeleteAsync($"api/DataFlow/{Guid.NewGuid()}");
    
    Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
  }
}