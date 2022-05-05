using System;
using Flooq.Api.Metrics.Registries;
using Flooq.Api.Metrics.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Flooq.Test.Metrics;

[TestClass]
public class LinearizedGraphMetricsServiceTest
{
  private readonly Random _random = new();
  private int _n;

  [TestInitialize]
  public void Setup()
  {
    _n = _random.Next(11);
  }
  
  [TestMethod]
  public void CanCreateLinearizedGraphMetricsService()
  {
    var metricsService = new LinearizedGraphMetricsService();
    Assert.IsNotNull(metricsService);
  }

  [TestMethod]
  public void CanIncrementRequestedListsCount()
  {
    Assert.AreEqual(0, LinearizedGraphRegistry.RequestedListsCount.Value);
    
    var metricsService = new LinearizedGraphMetricsService();
    
    for (var i = 0; i < _n; i++)
    {
      metricsService.IncrementRequestedListsCount();
    }
    
    Assert.AreEqual(_n, LinearizedGraphRegistry.RequestedListsCount.Value);
  }

  [TestMethod]
  public void CanIncrementRequestedByIdCount()
  {
    Assert.AreEqual(0, LinearizedGraphRegistry.RequestedByIdCount.Value);

    var metricsService = new LinearizedGraphMetricsService();
    
    for (var i = 0; i < _n; i++)
    {
      metricsService.IncrementRequestedByIdCount();
    }
    
    Assert.AreEqual(_n, LinearizedGraphRegistry.RequestedByIdCount.Value);
  }
  
  [TestMethod]
  public void CanIncrementCreatedCount()
  {
    Assert.AreEqual(0, LinearizedGraphRegistry.CreatedCount.Value);

    var metricsService = new LinearizedGraphMetricsService();
    
    for (var i = 0; i < _n; i++)
    {
      metricsService.IncrementCreatedCount();
    }
    
    Assert.AreEqual(_n, LinearizedGraphRegistry.CreatedCount.Value);
  }
  
  [TestMethod]
  public void CanIncrementNotFoundCount()
  {
    Assert.AreEqual(0, LinearizedGraphRegistry.NotFoundCount.Value);

    var metricsService = new LinearizedGraphMetricsService();
    
    for (var i = 0; i < _n; i++)
    {
      metricsService.IncrementNotFoundCount();
    }
    
    Assert.AreEqual(_n, LinearizedGraphRegistry.NotFoundCount.Value);
  }
  
  [TestMethod]
  public void CanIncrementBadRequestCount()
  {
    Assert.AreEqual(0, LinearizedGraphRegistry.BadRequestCount.Value);

    var metricsService = new LinearizedGraphMetricsService();
    
    for (var i = 0; i < _n; i++)
    {
      metricsService.IncrementBadRequestCount();
    }
    
    Assert.AreEqual(_n, LinearizedGraphRegistry.BadRequestCount.Value);
  }
}