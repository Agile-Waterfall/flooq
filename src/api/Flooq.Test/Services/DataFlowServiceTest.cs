using System;
using System.Collections.Generic;
using Flooq.Api.Domain;
using Flooq.Api.Models;
using Flooq.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Flooq.Test.Services;

[TestClass]
public class DataFlowServiceTest
{
  private FlooqContext _context;
  
  [TestInitialize]
  public void Setup()
  {
    var options = new DbContextOptionsBuilder<FlooqContext>().UseInMemoryDatabase(databaseName: "FlooqDatabase").Options;
    _context = new FlooqContext(options);
  }

  [TestMethod]
  public void CanCreateDataFlowController()
  {
    var versionService = new DataFlowService(_context);
    Assert.IsNotNull(versionService);
  }

  [TestMethod]
  public void CanGetDataFlows()
  {
    var dataFlow = new DataFlow
    {
      Id = Guid.NewGuid(),
      Name = "Demo Flow",
      Status = "Active",
      LastEdited = DateTime.Now,
      Definition = "{}"
    };

    _context.DataFlows.Add(dataFlow);
    _context.SaveChanges();

    var dataFlows = new List<DataFlow>();
    dataFlows.Add(dataFlow);
    var actionResult = new ActionResult<IEnumerable<DataFlow>>(dataFlows);

    var dataFlowService = new DataFlowService(_context);

    Assert.AreSame(actionResult.Value.GetEnumerator().Current,
      dataFlowService.GetDataFlows().Result.Value.GetEnumerator().Current);
  }

  [TestMethod]
  public void CanGetDataFlow()
  {
    var dataFlow = new DataFlow
    {
      Id = Guid.NewGuid(),
      Name = "Demo Flow",
      Status = "Active",
      LastEdited = DateTime.Now,
      Definition = "{}"
    };
    
    _context.DataFlows.Add(dataFlow);
    _context.SaveChanges();

    var DataFlowService = new DataFlowService(_context);
    
    Assert.AreSame(dataFlow, DataFlowService.GetDataFlow(dataFlow.Id).Result.Value);
  }

  [TestMethod]
  public void CanSetEntryState()
  {
    var dataFlow = new DataFlow
    {
      Id = Guid.NewGuid(),
      Name = "Demo Flow",
      Status = "Active",
      LastEdited = DateTime.Now,
      Definition = "{}"
    };

    var DataFlowService = new DataFlowService(_context);
    
    DataFlowService.SetEntryState(dataFlow, EntityState.Modified);
    Assert.AreEqual(EntityState.Modified, _context.Entry(dataFlow).State);
  }

  [TestMethod]
  public void CanAddDataFlow()
  {
    var dataFlow = new DataFlow
    {
      Id = Guid.NewGuid(),
      Name = "Demo Flow",
      Status = "Active",
      LastEdited = DateTime.Now,
      Definition = "{}"
    };

    var DataFlowService = new DataFlowService(_context);
    
    Assert.IsNotNull(DataFlowService.AddDataFlow(dataFlow));
  }

  [TestMethod]
  public void CanRemoveDataFlow()
  {
    var dataFlow = new DataFlow
    {
      Id = Guid.NewGuid(),
      Name = "Demo Flow",
      Status = "Active",
      LastEdited = DateTime.Now,
      Definition = "{}"
    };

    var DataFlowService = new DataFlowService(_context);
    DataFlowService.AddDataFlow(dataFlow);

    Assert.IsNotNull(DataFlowService.RemoveDataFlow(dataFlow));
  }

  [TestMethod]
  public void TestDataFlowExists()
  {
    var dataFlow = new DataFlow
    {
      Id = Guid.NewGuid(),
      Name = "Demo Flow",
      Status = "Active",
      LastEdited = DateTime.Now,
      Definition = "{}"
    };

    var DataFlowService = new DataFlowService(_context);

    Assert.IsFalse(DataFlowService.DataFlowExists(dataFlow.Id));

    _context.DataFlows.Add(dataFlow);

    Assert.IsTrue(DataFlowService.DataFlowExists(dataFlow.Id));
  }
}