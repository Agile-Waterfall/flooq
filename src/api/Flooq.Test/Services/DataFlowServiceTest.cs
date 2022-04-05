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
  private readonly FlooqContext _context = new FlooqContext(new DbContextOptionsBuilder<FlooqContext>().UseInMemoryDatabase(databaseName: "FlooqDatabase").Options);
  private readonly DataFlow _dataFlow = new DataFlow
  {
    Id = Guid.NewGuid(),
    Name = "Demo Flow",
    Status = "Active",
    LastEdited = DateTime.Now,
    Definition = "{}"
  };
  
  [TestMethod]
  public void CanCreateDataFlowService()
  {
    var versionService = new DataFlowService(_context);
    Assert.IsNotNull(versionService);
  }

  [TestMethod]
  public void CanGetDataFlows()
  {
    Assert.IsNotNull(_context.DataFlows);
    
    _context.DataFlows.Add(_dataFlow);
    _context.SaveChanges();

    var dataFlows = new List<DataFlow>();
    dataFlows.Add(_dataFlow);
    var actionResult = new ActionResult<IEnumerable<DataFlow>>(dataFlows);

    var dataFlowService = new DataFlowService(_context);

    Assert.AreSame(actionResult.Value?.GetEnumerator().Current,
      dataFlowService.GetDataFlows().Result.Value?.GetEnumerator().Current);
  }

  [TestMethod]
  public void CanGetDataFlow()
  {
    Assert.IsNotNull(_context.DataFlows);
    
    _context.DataFlows.Add(_dataFlow);
    _context.SaveChanges();

    var dataFlowService = new DataFlowService(_context);
    
    Assert.AreSame(_dataFlow, dataFlowService.GetDataFlow(_dataFlow.Id).Result.Value);
  }

  [TestMethod]
  public void CanSetEntryState()
  {
    var dataFlowService = new DataFlowService(_context);
    
    dataFlowService.SetEntryState(_dataFlow, EntityState.Modified);
    Assert.AreEqual(EntityState.Modified, _context.Entry(_dataFlow).State);
  }

  [TestMethod]
  public void CanAddDataFlow()
  {
    var dataFlowService = new DataFlowService(_context);
    
    Assert.IsNotNull(dataFlowService.AddDataFlow(_dataFlow));
  }

  [TestMethod]
  public void CanRemoveDataFlow()
  {
    var dataFlowService = new DataFlowService(_context);
    dataFlowService.AddDataFlow(_dataFlow);

    Assert.IsNotNull(dataFlowService.RemoveDataFlow(_dataFlow));
  }

  [TestMethod]
  public void TestDataFlowExists()
  {
    var dataFlowService = new DataFlowService(_context);

    Assert.IsFalse(dataFlowService.DataFlowExists(_dataFlow.Id));

    dataFlowService.AddDataFlow(_dataFlow);
    dataFlowService.SaveChangesAsync();

    Assert.IsTrue(dataFlowService.DataFlowExists(_dataFlow.Id));
  }
}