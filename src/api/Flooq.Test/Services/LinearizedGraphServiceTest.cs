using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flooq.Api.Domain;
using Flooq.Api.Models;
using Flooq.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Flooq.Test.Services;

[TestClass]
public class LinearizedGraphServiceTest
{
  private FlooqContext _context = null!;
  private readonly LinearizedGraph _graph = new()
  {
    Id = Guid.NewGuid(),
    Graph = ""
  };
  
  [TestInitialize]
  public async Task Setup()
  {
    var config = new ConfigurationManager();
    config.AddJsonFile("appsettings.Test.json");
    _context = new FlooqContext(new DbContextOptionsBuilder<FlooqContext>()
      .UseInMemoryDatabase(databaseName: "FlooqDatabase").Options, config);
    
    foreach (var graph in _context.Graphs) _context.Graphs.Remove(graph);
    await _context.SaveChangesAsync();
  }

  [TestMethod]
  public void CanCreateLinearizedGraphService()
  {
    Assert.IsNotNull(_context.Graphs);
    var graphService = new LinearizedGraphService(_context);
    Assert.IsNotNull(graphService);
  }

  [TestMethod]
  public async Task CanGetLinearizedGraphs()
  {
    var graphService = new LinearizedGraphService(_context);
    
    var actionResult = await graphService.GetGraphs();
    Assert.AreEqual(0, actionResult.Value?.Count());

    _context.Graphs.Add(_graph);
    await _context.SaveChangesAsync();

    actionResult = await graphService.GetGraphs();
    Assert.AreEqual(1, actionResult.Value?.Count());
    
    var graphs = new List<LinearizedGraph> {_graph};
    var testActionResult = new ActionResult<IEnumerable<LinearizedGraph>>(graphs);

    Assert.AreSame(
      testActionResult.Value?.GetEnumerator().Current,
      actionResult.Value?.GetEnumerator().Current
      );
  }
  
  [TestMethod]
  public async Task CanGetLinearizedGraph()
  {
    var graphService = new LinearizedGraphService(_context);
    
    _context.Graphs.Add(_graph);
    await _context.SaveChangesAsync();

    var actionResult = await graphService.GetGraph(_graph.Id);
    var graph = actionResult.Value;
    
    Assert.AreSame(_graph, graph);
  }
  
  [TestMethod]
  public async Task CanAddLinearizedGraphAndSaveChangesAsync()
  {
    var graphService = new LinearizedGraphService(_context);

    var actionResult = await graphService.GetGraph(_graph.Id);
    var graph = actionResult.Value;
    Assert.IsNull(graph);
    
    graphService.AddGraph(_graph);
    await graphService.SaveChangesAsync();
    actionResult = await graphService.GetGraph(_graph.Id);
    graph = actionResult.Value;
    Assert.IsNotNull(graph);
    Assert.AreEqual(_graph.Id, graph.Id);
  }
  
  [TestMethod]
  public async Task CanRemoveLinearizedGraph()
  {
    var graphService = new LinearizedGraphService(_context);

    _context.Graphs.Add(_graph);
    await _context.SaveChangesAsync();
    var graph = await _context.Graphs.FindAsync(_graph.Id);
    Assert.IsNotNull(graph);
    Assert.AreEqual(1, _context.Graphs.Count());
    
    var removedGraph = graphService.RemoveGraph(_graph).Entity;
    await _context.SaveChangesAsync();
    Assert.IsNotNull(removedGraph);
    Assert.AreEqual(_graph.Id, graph.Id);
    Assert.AreEqual(0, _context.Graphs.Count());
  }

  [TestMethod]
  public async Task TestLinearizedGraphExists()
  {
    var graphService = new LinearizedGraphService(_context);

    Assert.IsFalse(graphService.LinearizedGraphExists(_graph.Id));

    graphService.AddGraph(_graph);
    await graphService.SaveChangesAsync();

    Assert.IsTrue(graphService.LinearizedGraphExists(_graph.Id));
  }
}