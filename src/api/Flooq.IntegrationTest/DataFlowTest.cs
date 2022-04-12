using System;
using System.Net.Http;
using Flooq.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Flooq.IntegrationTest;

[TestClass]
public class DataFlowTest
{
  private FlooqWebApplicationFactory<Program> _factory;

  [TestInitialize]
  public void Setup()
  {
    _factory = new FlooqWebApplicationFactory<Program>();
  }

  [TestMethod]
  public void CanGetDataFlows()
  {
    _CanGetDataFlows();
  }

  private async void _CanGetDataFlows()
  {
    var client = _factory.CreateClient();

    var response = await client.GetAsync("api/DataFlow");

    response.EnsureSuccessStatusCode();
    Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
    Assert.IsTrue(response.Content.ToString().Contains(FlooqWebApplicationFactory<Program>.TEST_GUID.ToString()));
  }

  [TestMethod]
  public void CanGetDataFlow()
  {
    _CanGetDataFlow();
  }
  
  private async void _CanGetDataFlow()
  {
    var client = _factory.CreateClient();

    var response = await client.GetAsync($"api/DataFlow/{FlooqWebApplicationFactory<Program>.TEST_GUID}");

    response.EnsureSuccessStatusCode();
    Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
    Assert.IsTrue(response.Content.ToString().Contains(FlooqWebApplicationFactory<Program>.TEST_GUID.ToString()));
  }
  
  [TestMethod]
  public void CannotGetNonExistingDataFlow()
  {
    _CannotGetNonExistingDataFlow();
  }
  
  private async void _CannotGetNonExistingDataFlow()
  {
    var client = _factory.CreateClient();

    var response = await client.GetAsync($"api/DataFlow/{Guid.NewGuid()}");

    Assert.Equals(StatusCodes.Status404NotFound, response.StatusCode);
  }

  [TestMethod]
  public void CanPostDataFlow()
  {
    _CanPostDataFlow();
  }
  
  private async void _CanPostDataFlow()
  {
    var client = _factory.CreateClient();
    var dataFlow = new DataFlow()
    {
      Id = Guid.NewGuid(),
      Name = "Demo Flow",
      Status = "Active",
      LastEdited = DateTime.Now,
      Definition = "{}"
    };

    var response = await client.PostAsync("api/DataFlow", new StringContent(dataFlow.ToString()));

    response.EnsureSuccessStatusCode();
    Assert.Equals(StatusCodes.Status201Created, response.StatusCode);
  }

  [TestMethod]
  public void CanPutDataFlow()
  {
    _CanPutDataFlow();
  }
  
  private async void _CanPutDataFlow()
  {
    var client = _factory.CreateClient();
    var dataFlow = new DataFlow()
    {
      Id = FlooqWebApplicationFactory<Program>.TEST_GUID,
      Name = "Demo Flow 2",
      Status = "Active",
      LastEdited = DateTime.Now,
      Definition = "{}"
    };

    var response = await client.PutAsync($"api/DataFlow/{FlooqWebApplicationFactory<Program>.TEST_GUID}", 
      new StringContent(dataFlow.ToString()));

    response.EnsureSuccessStatusCode();
    Assert.Equals(StatusCodes.Status204NoContent, response.StatusCode);
  }
  
  [TestMethod]
  public void CannotPutNonExistingDataFlow()
  {
    _CannotPutNonExistingDataFlow();
  }
  
  private async void _CannotPutNonExistingDataFlow()
  {
    var client = _factory.CreateClient();
    var id = Guid.NewGuid();
    var dataFlow = new DataFlow()
    {
      Id = id,
      Name = "Demo Flow 2",
      Status = "Active",
      LastEdited = DateTime.Now,
      Definition = "{}"
    };

    var response = await client.PutAsync($"api/DataFlow/{id}", new StringContent(dataFlow.ToString()));
    
    Assert.Equals(StatusCodes.Status404NotFound, response.StatusCode);
  }
  
  [TestMethod]
  public void CannotPutDataFlowWithWrongId()
  {
    _CannotPutDataFlowWithWrongId();
  }
  
  private async void _CannotPutDataFlowWithWrongId()
  {
    var client = _factory.CreateClient();
    var id = Guid.NewGuid();
    var dataFlow = new DataFlow()
    {
      Id = id,
      Name = "Demo Flow 2",
      Status = "Active",
      LastEdited = DateTime.Now,
      Definition = "{}"
    };

    var response = await client.PutAsync($"api/DataFlow/{FlooqWebApplicationFactory<Program>.TEST_GUID}", 
      new StringContent(dataFlow.ToString()));
    
    Assert.Equals(StatusCodes.Status400BadRequest, response.StatusCode);
  }
  
  [TestMethod]
  public void CanDeleteDataFlow()
  {
    _CanDeleteDataFlow();
  }
  
  private async void _CanDeleteDataFlow()
  {
    var client = _factory.CreateClient();

    var response = await client.DeleteAsync($"api/DataFlow/{FlooqWebApplicationFactory<Program>.TEST_GUID}");

    response.EnsureSuccessStatusCode();
    Assert.Equals(StatusCodes.Status204NoContent, response.StatusCode);
  }
  
  [TestMethod]
  public void CannotDeleteNonExistingDataFlow()
  {
    _CannotDeleteNonExistingDataFlow();
  }
  
  private async void _CannotDeleteNonExistingDataFlow()
  {
    var client = _factory.CreateClient();

    var response = await client.DeleteAsync($"api/DataFlow/{Guid.NewGuid()}");
    
    Assert.Equals(StatusCodes.Status404NotFound, response.StatusCode);
  }
}