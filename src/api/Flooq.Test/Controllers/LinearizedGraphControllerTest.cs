using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flooq.Api.Controllers;
using Flooq.Api.Metrics.Services;
using Flooq.Api.Models;
using Flooq.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Flooq.Test.Controllers;

[TestClass]
public class LinearizedGraphControllerTest
{
  private readonly LinearizedGraph _graph = new()
  {
    Id = Guid.NewGuid(),
    Graph = ""
  };

  private readonly Mock<ILinearizedGraphService> _graphServiceMock = new();
  private readonly Mock<ILinearizedGraphMetricsService> _metricsServiceMock = new();

  [TestMethod]
  public void CanCreateLinearizedGraphController()
  {
    var graphController = new LinearizedGraphController(_graphServiceMock.Object, _metricsServiceMock.Object);
    
    Assert.IsNotNull(graphController);
  }
  
  [TestMethod]
  public async Task CanGetLinearizedGraphs_Zero()
  {
    var graphs = new List<LinearizedGraph>(); 
    var actionResult = new ActionResult<IEnumerable<LinearizedGraph>>(graphs);
    _graphServiceMock.Setup(service => service.GetGraphs()).ReturnsAsync(actionResult);
    var graphController = new LinearizedGraphController(_graphServiceMock.Object, _metricsServiceMock.Object);

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
    var graphController = new LinearizedGraphController(_graphServiceMock.Object, _metricsServiceMock.Object);

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
    var graphController = new LinearizedGraphController(_graphServiceMock.Object, _metricsServiceMock.Object);

    var actionResultReceived = await graphController.GetGraphs();
    
    Assert.AreSame(actionResult, actionResultReceived);
  }
  
  [TestMethod]
  public async Task CanGetLinearizedGraph()
  {
    _graphServiceMock.Setup(service => service.GetGraph(_graph.Id)).ReturnsAsync(_graph);
    var graphController = new LinearizedGraphController(_graphServiceMock.Object, _metricsServiceMock.Object);

    var actionResultReceived = await graphController.GetGraph(_graph.Id);
    var graphReceived = actionResultReceived.Value;
    
    Assert.AreSame(_graph, graphReceived);
  }
  
  [TestMethod]
  public async Task Get_ReturnsNotFoundIfThereIsNoMatchingLinearizedGraph()
  {
    var newId = Guid.NewGuid();
    _graphServiceMock.Setup(service => service.GetGraph(newId)).ReturnsAsync(new ActionResult<LinearizedGraph?>((LinearizedGraph?) null));
    var graphController = new LinearizedGraphController(_graphServiceMock.Object, _metricsServiceMock.Object);

    var actionResult = await graphController.GetGraph(newId);
    
    Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
  }
  
  [TestMethod]
  public async Task CanPostLinearizedGraph()
  {
    var graphController = new LinearizedGraphController(_graphServiceMock.Object, _metricsServiceMock.Object);

    var actionResult = await graphController.PostGraph(_graph);
    
    Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult));

    var createdAtAction = actionResult.Result as CreatedAtActionResult;

    Assert.IsNotNull(createdAtAction?.Value);
    Assert.AreSame(_graph, createdAtAction.Value);
  }
  
  [TestMethod]
  public async Task Post_ReturnsConflictIfLinearizedGraphAlreadyExists()
  {
    _graphServiceMock.Setup(service => service.LinearizedGraphExists(_graph.Id)).Returns(true);
    _graphServiceMock.Setup(service => service.SaveChangesAsync()).ThrowsAsync(new ArgumentException());
    var graphController = new LinearizedGraphController(_graphServiceMock.Object, _metricsServiceMock.Object);

    var actionResult = await graphController.PostGraph(_graph);
    
    Assert.IsInstanceOfType(actionResult.Result, typeof(ConflictResult));
  }

  [TestMethod]
  [ExpectedException(typeof(DbUpdateException))]
  public async Task Post_ThrowsExceptionIfLinearizedGraphDoesNotExistButCannotBePosted() 
  {
    _graphServiceMock.Setup(service => service.SaveChangesAsync()).ThrowsAsync(new DbUpdateException());
    _graphServiceMock.Setup(service => service.LinearizedGraphExists(_graph.Id)).Returns(false);
    
    var graphController = new LinearizedGraphController(_graphServiceMock.Object, _metricsServiceMock.Object);


    await graphController.PostGraph(_graph);
  }
}