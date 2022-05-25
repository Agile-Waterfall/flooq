using System;
using Flooq.Api.Metrics.Registries;
using Flooq.Api.Metrics.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Flooq.Test.Metrics;

[TestClass]
public class TokenMetricsServiceTest
{
  private readonly Random _random = new();
  private int _n;

  [TestInitialize]
  public void Setup()
  {
    _n = _random.Next(1, 11);
  }
  
  [TestMethod]
  public void CanCreateTokenMetricsService()
  {
    var metricsService = new TokenMetricsService();
    
    Assert.IsNotNull(metricsService);
  }

  [TestMethod]
  public void CanIncrementRequestedListsCount()
  {
    Assert.AreEqual(0, TokenRegistry.RequestedListsCount.Value);
    
    var metricsService = new TokenMetricsService();
    
    for (var i = 0; i < _n; i++)
    {
      metricsService.IncrementRequestedListsCount();
    }
    
    Assert.AreEqual(_n, TokenRegistry.RequestedListsCount.Value);
  }

  [TestMethod]
  public void CanIncrementRequestedByIdCount()
  {
    Assert.AreEqual(0, TokenRegistry.RequestedByIdCount.Value);

    var metricsService = new TokenMetricsService();
    
    for (var i = 0; i < _n; i++)
    {
      metricsService.IncrementRequestedByIdCount();
    }
    
    Assert.AreEqual(_n, TokenRegistry.RequestedByIdCount.Value);
  }
  
  [TestMethod]
  public void CanIncrementEditedCount()
  {
    Assert.AreEqual(0, TokenRegistry.EditedCount.Value);

    var metricsService = new TokenMetricsService();
    
    for (var i = 0; i < _n; i++)
    {
      metricsService.IncrementEditedCount();
    }
    
    Assert.AreEqual(_n, TokenRegistry.EditedCount.Value);
  }
  
  [TestMethod]
  public void CanIncrementCreatedCount()
  {
    Assert.AreEqual(0, TokenRegistry.CreatedCount.Value);

    var metricsService = new TokenMetricsService();
    
    for (var i = 0; i < _n; i++)
    {
      metricsService.IncrementCreatedCount();
    }
    
    Assert.AreEqual(_n, TokenRegistry.CreatedCount.Value);
  }
  
  [TestMethod]
  public void CanIncrementDeletedCount()
  {
    Assert.AreEqual(0, TokenRegistry.DeletedCount.Value);

    var metricsService = new TokenMetricsService();
    
    for (var i = 0; i < _n; i++)
    {
      metricsService.IncrementDeletedCount();
    }
    
    Assert.AreEqual(_n, TokenRegistry.DeletedCount.Value);
  }
  
  [TestMethod]
  public void CanIncrementNotFoundCount()
  {
    Assert.AreEqual(0, TokenRegistry.NotFoundCount.Value);

    var metricsService = new TokenMetricsService();
    
    for (var i = 0; i < _n; i++)
    {
      metricsService.IncrementNotFoundCount();
    }
    
    Assert.AreEqual(_n, TokenRegistry.NotFoundCount.Value);
  }
  
  [TestMethod]
  public void CanIncrementBadRequestCount()
  {
    Assert.AreEqual(0, TokenRegistry.BadRequestCount.Value);

    var metricsService = new TokenMetricsService();
    
    for (var i = 0; i < _n; i++)
    {
      metricsService.IncrementBadRequestCount();
    }
    
    Assert.AreEqual(_n, TokenRegistry.BadRequestCount.Value);
  }
  
  [TestMethod]
  public void CanIncrementExceptionCount()
  {
    Assert.AreEqual(0, TokenRegistry.ExceptionCount.Value);

    var metricsService = new TokenMetricsService();
    
    for (var i = 0; i < _n; i++)
    {
      metricsService.IncrementExceptionCount();
    }
    
    Assert.AreEqual(_n, TokenRegistry.ExceptionCount.Value);
  }
  
  [TestMethod]
  public void CanIncrementUnauthorizedCount()
  {
    Assert.AreEqual(0, TokenRegistry.UnauthorizedCount.Value);

    var metricsService = new TokenMetricsService();
    
    for (var i = 0; i < _n; i++)
    {
      metricsService.IncrementUnauthorizedCount();
    }
    
    Assert.AreEqual(_n, TokenRegistry.UnauthorizedCount.Value);
  }
  
  [TestMethod]
  public void CanIncrementConflictCount()
  {
    Assert.AreEqual(0, TokenRegistry.ConflictCount.Value);

    var metricsService = new TokenMetricsService();
    
    for (var i = 0; i < _n; i++)
    {
      metricsService.IncrementConflictCount();
    }
    
    Assert.AreEqual(_n, TokenRegistry.ConflictCount.Value);
  }
}