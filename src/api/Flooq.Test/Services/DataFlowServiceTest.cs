using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
  private readonly FlooqContext _context = new (new DbContextOptionsBuilder<FlooqContext>().UseInMemoryDatabase(databaseName: "FlooqDatabase").Options);
  private readonly DataFlow _dataFlow = new() 
  {
    Id = Guid.NewGuid(),
    Name = "Demo Flow",
    Status = "Active",
    LastEdited = DateTime.Now,
    Definition = "{}"
  };

  [TestInitialize]
  public async Task TestInitialize()
  {
    foreach (var dataFlow in _context.DataFlows) _context.DataFlows.Remove(dataFlow);
    await _context.SaveChangesAsync();
  }
  
  [TestMethod]
  public void CanCreateDataFlowService()
  {
    Assert.IsNotNull(_context.DataFlows);
    var dataFlowService = new DataFlowService(_context);
    Assert.IsNotNull(dataFlowService);
  }

  [TestMethod]
  public async Task CanGetDataFlows()
  {
    var dataFlowService = new DataFlowService(_context);
    
    var actionResult = await dataFlowService.GetDataFlows();
    Assert.AreEqual(0, actionResult.Value?.Count());

    _context.DataFlows.Add(_dataFlow);
    await _context.SaveChangesAsync();

    actionResult = await dataFlowService.GetDataFlows();
    Assert.AreEqual(1, actionResult.Value?.Count());
    
    var dataFlows = new List<DataFlow> {_dataFlow};
    var testActionResult = new ActionResult<IEnumerable<DataFlow>>(dataFlows);

    Assert.AreSame(
      testActionResult.Value?.GetEnumerator().Current,
      actionResult.Value?.GetEnumerator().Current
      );
  }

  [TestMethod]
  public async Task CanGetDataFlow()
  {
    var dataFlowService = new DataFlowService(_context);
    
    _context.DataFlows.Add(_dataFlow);
    await _context.SaveChangesAsync();

    var actionResult = await dataFlowService.GetDataFlow(_dataFlow.Id);
    var dataFlow = actionResult.Value;
    
    Assert.AreSame(_dataFlow, dataFlow);
  }

  [TestMethod]
  public async Task CanPutDataFlow()
  {
    var dataFlowService = new DataFlowService(_context);

    _context.DataFlows.Add(_dataFlow);
    await dataFlowService.SaveChangesAsync();
    
    var actionResultDataFlows = await dataFlowService.GetDataFlows();
    Assert.AreEqual(1, actionResultDataFlows.Value?.Count());

    const string newName = "Changed Flow";
    
    var newDataFlow = new DataFlow
    {
      Id = _dataFlow.Id,
      Name = newName,
      Status = "Active",
      LastEdited = DateTime.Now,
      Definition = "{}"
    };

    // Need to detach the tracked instance _dataFlow. Don't know where the tracking started.
    // https://stackoverflow.com/questions/62253837/the-instance-of-entity-type-cannot-be-tracked-because-another-instance-with-the
    _context.Entry(_dataFlow).State = EntityState.Detached;
    
    var actionResult = await dataFlowService.PutDataFlow(newDataFlow);
    var dataFlow = actionResult.Value;
    Assert.AreEqual(EntityState.Unchanged, _context.Entry(newDataFlow).State);
    Assert.AreNotEqual(_dataFlow, dataFlow);
    Assert.AreEqual(newName, dataFlow?.Name);
  }

  [TestMethod]
  public async Task CanAddDataFlowAndSaveChangesAsync()
  {
    var dataFlowService = new DataFlowService(_context);

    var actionResult = await dataFlowService.GetDataFlow(_dataFlow.Id);
    var dataFlow = actionResult.Value;
    Assert.IsNull(dataFlow);
    
    dataFlowService.AddDataFlow(_dataFlow);
    await dataFlowService.SaveChangesAsync();
    actionResult = await dataFlowService.GetDataFlow(_dataFlow.Id);
    dataFlow = actionResult.Value;
    Assert.IsNotNull(dataFlow);
    Assert.AreEqual(_dataFlow.Id, dataFlow.Id);
  }

  [TestMethod]
  public async Task CanRemoveDataFlow()
  {
    var dataFlowService = new DataFlowService(_context);

    _context.DataFlows.Add(_dataFlow);
    await _context.SaveChangesAsync();
    var dataFlow = await _context.DataFlows.FindAsync(_dataFlow.Id);
    Assert.IsNotNull(dataFlow);
    Assert.AreEqual(1, _context.DataFlows.Count());
    
    var removedDataFlow = dataFlowService.RemoveDataFlow(_dataFlow).Entity;
    await _context.SaveChangesAsync();
    Assert.IsNotNull(removedDataFlow);
    Assert.AreEqual(_dataFlow.Id, dataFlow.Id);
    Assert.AreEqual(0, _context.DataFlows.Count());
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