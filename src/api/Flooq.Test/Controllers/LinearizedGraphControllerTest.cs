using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flooq.Api.Controllers;
using Flooq.Api.Models;
using Flooq.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Flooq.Test.Controllers;

[TestClass]
public class LinearizedGraphControllerTest
{
  private readonly Mock<ILinearizedGraphService> _graphServiceMock = new();
  private readonly LinearizedGraph _graph = new()
  {
    Id = Guid.NewGuid(),
    Graph = ""
  };

  [TestMethod]
  public void CanCreateLinearizedGraphController()
  {
    var graphController = new LinearizedGraphController(_graphServiceMock.Object);
    Assert.IsNotNull(graphController);
  }
  
  [TestMethod]
  public async Task CanGetLinearizedGraphs_Zero()
  {
    var graphs = new List<LinearizedGraph>(); 
    var actionResult = new ActionResult<IEnumerable<LinearizedGraph>>(graphs);
    _graphServiceMock.Setup(service => service.GetGraphs()).ReturnsAsync(actionResult);
    var graphController = new LinearizedGraphController(_graphServiceMock.Object);

    var actionResultReceived = await graphController.GetGraphs();
    Assert.AreSame(actionResult, actionResultReceived);
    Assert.IsNull(actionResultReceived.Value?.GetEnumerator().Current);
  }
  
  [TestMethod]
  public async Task CanGetLinearizedGraphs_One()
  {
    var graphs = new List<LinearizedGraph> {_graph}; 
    var actionResult = new ActionResult<IEnumerable<LinearizedGraph>>(graphs);
    _graphServiceMock.Setup(service => service.GetGraphs()).ReturnsAsync(actionResult);
    var graphController = new LinearizedGraphController(_graphServiceMock.Object);

    var actionResultReceived = await graphController.GetGraphs();
    Assert.AreSame(actionResult, actionResultReceived);
  }
  
  [TestMethod]
  public async Task CanGetLinearizedGraphs_Multiple()
  {
    LinearizedGraph graph = new()
    {
      Id = Guid.NewGuid(),
      Graph = ""
    };
    var graphs = new List<LinearizedGraph> {_graph, graph};
    var actionResult = new ActionResult<IEnumerable<LinearizedGraph>>(graphs);
    _graphServiceMock.Setup(service => service.GetGraphs()).ReturnsAsync(actionResult);
    var graphController = new LinearizedGraphController(_graphServiceMock.Object);

    var actionResultReceived = await graphController.GetGraphs();
    Assert.AreSame(actionResult, actionResultReceived);
  }
  
  [TestMethod]
  public async Task CanGetDataFlow()
  {
    _graphServiceMock.Setup(service => service.GetGraph(_graph.Id)).ReturnsAsync(_graph);
    var graphController = new LinearizedGraphController(_graphServiceMock.Object);

    var actionResultReceived = await graphController.GetGraph(_graph.Id);
    var graphReceived = actionResultReceived.Value;
    Assert.AreSame(_graph, graphReceived);
  }
  
  [TestMethod]
  public async Task Get_ReturnsNotFoundIfThereIsNoMatchingDataFlow()
  {
    _graphServiceMock.Setup(service => service.GetGraph(_graph.Id)).ReturnsAsync(new ActionResult<LinearizedGraph?>(_graph));
    var newId = Guid.NewGuid();
    _graphServiceMock.Setup(service => service.GetGraph(newId)).ReturnsAsync(new ActionResult<LinearizedGraph?>((LinearizedGraph?) null));
    var graphController = new LinearizedGraphController(_graphServiceMock.Object);

    var actionResult = await graphController.GetGraph(_graph.Id);
    Assert.IsInstanceOfType(actionResult, typeof(ActionResult<LinearizedGraph>));
    
    var graph = actionResult.Value;
    Assert.IsNotNull(graph);
    Assert.AreSame(_graph, graph);

    actionResult = await graphController.GetGraph(newId);
    Assert.IsInstanceOfType(actionResult, typeof(ActionResult<LinearizedGraph>));
    
    graph = actionResult.Value;
    Assert.IsNull(graph);
  }
  
  [TestMethod]
  public async Task CanPostDataFlow()
  {
    var graphController = new LinearizedGraphController(_graphServiceMock.Object);

    var actionResult = await graphController.PostGraph(_graph);
    Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult));

    var createdAtAction = (actionResult.Result as CreatedAtActionResult);

    Assert.IsNotNull(createdAtAction?.Value);
    Assert.AreSame(_graph, createdAtAction.Value);
  }
  
  [TestMethod]
  public async Task Post_ReturnsBadRequestIfLinearizedGraphAlreadyExists()
  {
    _graphServiceMock.Setup(service => service.LinearizedGraphExists(_graph.Id)).Returns(true);
    var graphController = new LinearizedGraphController(_graphServiceMock.Object);

    var actionResult = await graphController.PostGraph(_graph);
    Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestResult));
  }
}