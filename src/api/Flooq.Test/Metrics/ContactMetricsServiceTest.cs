using System;
using Flooq.Api.Metrics.Registries;
using Flooq.Api.Metrics.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Flooq.Test.Metrics;

[TestClass]
public class ContactMetricsServiceTest
{
  private readonly Random _random = new();
  private int _n;

  [TestInitialize]
  public void Setup()
  {
    _n = _random.Next(1, 11);
  }
  
  [TestMethod]
  public void CanCreateContactMetricsService()
  {
    var metricsService = new ContactMetricsService();
    Assert.IsNotNull(metricsService);
  }

  [TestMethod]
  public void CanIncrementRequestedListsCount()
  {
    Assert.AreEqual(0, ContactRegistry.RequestedListsCount.Value);
    
    var metricsService = new ContactMetricsService();
    
    for (var i = 0; i < _n; i++)
    {
      metricsService.IncrementRequestedListsCount();
    }
    
    Assert.AreEqual(_n, ContactRegistry.RequestedListsCount.Value);
  }
  
  [TestMethod]
  public void CanIncrementCreatedCount()
  {
    Assert.AreEqual(0, ContactRegistry.CreatedCount.Value);

    var metricsService = new ContactMetricsService();
    
    for (var i = 0; i < _n; i++)
    {
      metricsService.IncrementCreatedCount();
    }
    
    Assert.AreEqual(_n, ContactRegistry.CreatedCount.Value);
  }
  
  [TestMethod]
  public void CanIncrementDeletedCount()
  {
    Assert.AreEqual(0, ContactRegistry.DeletedCount.Value);

    var metricsService = new ContactMetricsService();
    
    for (var i = 0; i < _n; i++)
    {
      metricsService.IncrementDeletedCount();
    }
    
    Assert.AreEqual(_n, ContactRegistry.DeletedCount.Value);
  }
  
  [TestMethod]
  public void CanIncrementNotFoundCount()
  {
    Assert.AreEqual(0, ContactRegistry.NotFoundCount.Value);

    var metricsService = new ContactMetricsService();
    
    for (var i = 0; i < _n; i++)
    {
      metricsService.IncrementNotFoundCount();
    }
    
    Assert.AreEqual(_n, ContactRegistry.NotFoundCount.Value);
  }
  
  [TestMethod]
  public void CanIncrementConflictCount()
  {
    Assert.AreEqual(0, ContactRegistry.ConflictCount.Value);

    var metricsService = new ContactMetricsService();
    
    for (var i = 0; i < _n; i++)
    {
      metricsService.IncrementConflictCount();
    }
    
    Assert.AreEqual(_n, ContactRegistry.ConflictCount.Value);
  }
  
  [TestMethod]
  public void CanIncrementExceptionCount()
  {
    Assert.AreEqual(0, ContactRegistry.ExceptionCount.Value);

    var metricsService = new ContactMetricsService();
    
    for (var i = 0; i < _n; i++)
    {
      metricsService.IncrementExceptionCount();
    }
    
    Assert.AreEqual(_n, ContactRegistry.ExceptionCount.Value);
  }
}