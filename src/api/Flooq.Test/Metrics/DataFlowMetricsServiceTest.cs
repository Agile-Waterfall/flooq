using System;
using Flooq.Api.Metrics.Registries;
using Flooq.Api.Metrics.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Flooq.Test.Metrics;

[TestClass]
public class DataFlowMetricsServiceTest
{
  private readonly Random _random = new();
  private int _n;

  [TestInitialize]
  public void Setup()
  {
    _n = _random.Next(1, 11);
  }
  
  [TestMethod]
  public void CanCreateDataFlowMetricsService()
  {
    var metricsService = new DataFlowMetricsService();
    
    Assert.IsNotNull(metricsService);
  }

  [TestMethod]
  public void CanIncrementRequestedListsCount()
  {
    Assert.AreEqual(0, DataFlowRegistry.RequestedListsCount.Value);
    
    var metricsService = new DataFlowMetricsService();
    
    for (var i = 0; i < _n; i++)
    {
      metricsService.IncrementRequestedListsCount();
    }
    
    Assert.AreEqual(_n, DataFlowRegistry.RequestedListsCount.Value);
  }

  [TestMethod]
  public void CanIncrementRequestedByIdCount()
  {
    Assert.AreEqual(0, DataFlowRegistry.RequestedByIdCount.Value);

    var metricsService = new DataFlowMetricsService();
    
    for (var i = 0; i < _n; i++)
    {
      metricsService.IncrementRequestedByIdCount();
    }
    
    Assert.AreEqual(_n, DataFlowRegistry.RequestedByIdCount.Value);
  }
  
  [TestMethod]
  public void CanIncrementEditedCount()
  {
    Assert.AreEqual(0, DataFlowRegistry.EditedCount.Value);

    var metricsService = new DataFlowMetricsService();
    
    for (var i = 0; i < _n; i++)
    {
      metricsService.IncrementEditedCount();
    }
    
    Assert.AreEqual(_n, DataFlowRegistry.EditedCount.Value);
  }
  
  [TestMethod]
  public void CanIncrementCreatedCount()
  {
    Assert.AreEqual(0, DataFlowRegistry.CreatedCount.Value);

    var metricsService = new DataFlowMetricsService();
    
    for (var i = 0; i < _n; i++)
    {
      metricsService.IncrementCreatedCount();
    }
    
    Assert.AreEqual(_n, DataFlowRegistry.CreatedCount.Value);
  }
  
  [TestMethod]
  public void CanIncrementDeletedCount()
  {
    Assert.AreEqual(0, DataFlowRegistry.DeletedCount.Value);

    var metricsService = new DataFlowMetricsService();
    
    for (var i = 0; i < _n; i++)
    {
      metricsService.IncrementDeletedCount();
    }
    
    Assert.AreEqual(_n, DataFlowRegistry.DeletedCount.Value);
  }
  
  [TestMethod]
  public void CanIncrementNotFoundCount()
  {
    Assert.AreEqual(0, DataFlowRegistry.NotFoundCount.Value);

    var metricsService = new DataFlowMetricsService();
    
    for (var i = 0; i < _n; i++)
    {
      metricsService.IncrementNotFoundCount();
    }
    
    Assert.AreEqual(_n, DataFlowRegistry.NotFoundCount.Value);
  }
  
  [TestMethod]
  public void CanIncrementBadRequestCount()
  {
    Assert.AreEqual(0, DataFlowRegistry.BadRequestCount.Value);

    var metricsService = new DataFlowMetricsService();
    
    for (var i = 0; i < _n; i++)
    {
      metricsService.IncrementBadRequestCount();
    }
    
    Assert.AreEqual(_n, DataFlowRegistry.BadRequestCount.Value);
  }
  
  [TestMethod]
  public void CanIncrementExceptionCount()
  {
    Assert.AreEqual(0, DataFlowRegistry.ExceptionCount.Value);

    var metricsService = new DataFlowMetricsService();
    
    for (var i = 0; i < _n; i++)
    {
      metricsService.IncrementExceptionCount();
    }
    
    Assert.AreEqual(_n, DataFlowRegistry.ExceptionCount.Value);
  }
}