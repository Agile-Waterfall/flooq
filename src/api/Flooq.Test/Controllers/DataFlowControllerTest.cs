using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Flooq.Api.Controllers;
using Flooq.Api.Models;
using Flooq.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flooq.Test.Controllers;

[TestClass]
public class DataFlowControllerTest
{
  private readonly Mock<IDataFlowService> _dataFlowServiceMock = new();
  private readonly Mock<ILinearizedGraphService> _graphServiceMock = new();
  private readonly DataFlow _dataFlow = new() 
  {
    Id = Guid.NewGuid(),
    Name = "Demo Flow",
    Status = "Active",
    LastEdited = DateTime.Now,
    Definition = "{}"
  };
  
  [TestMethod]
  public void CanCreateDataFlowController()
  {
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object);
    Assert.IsNotNull(dataFlowController);
  }
  
  [TestMethod]
  public async Task CanGetDataFlows_Zero()
  {
    var dataFlows = new List<DataFlow>(); 
    var actionResult = new ActionResult<IEnumerable<DataFlow>>(dataFlows);
    _dataFlowServiceMock.Setup(service => service.GetDataFlows()).ReturnsAsync(actionResult);
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object);

    var actionResultReceived = await dataFlowController.GetDataFlows();
    Assert.AreSame(actionResult, actionResultReceived);
    Assert.IsNull(actionResultReceived.Value?.GetEnumerator().Current);
  }

  [TestMethod]
  public async Task CanGetDataFlows_One()
  {
    var dataFlows = new List<DataFlow> {_dataFlow};
    var actionResult = new ActionResult<IEnumerable<DataFlow>>(dataFlows);
    _dataFlowServiceMock.Setup(service => service.GetDataFlows()).ReturnsAsync(actionResult);
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object);

    var actionResultReceived = await dataFlowController.GetDataFlows();
    Assert.AreSame(actionResult, actionResultReceived);
  }

  [TestMethod]
  public async Task CanGetDataFlows_Multiple()
  {
    DataFlow dataFlow2 = new()
    {
      Id = Guid.NewGuid(),
      Name = "Demo Flow 2",
      Status = "Active",
      LastEdited = DateTime.Now,
      Definition = "{}"
    };
    var dataFlows = new List<DataFlow> {_dataFlow, dataFlow2};
    var actionResult = new ActionResult<IEnumerable<DataFlow>>(dataFlows);
    _dataFlowServiceMock.Setup(service => service.GetDataFlows()).ReturnsAsync(actionResult);
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object);

    var actionResultReceived = await dataFlowController.GetDataFlows();
    Assert.AreSame(actionResult, actionResultReceived);
  }

  [TestMethod]
  public async Task CanGetDataFlow()
  {
    _dataFlowServiceMock.Setup(service => service.GetDataFlow(_dataFlow.Id)).ReturnsAsync(_dataFlow);
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object);

    var actionResultReceived = await dataFlowController.GetDataFlow(_dataFlow.Id);
    var dataFlowReceived = actionResultReceived.Value;
    Assert.AreSame(_dataFlow, dataFlowReceived);
  }

  [TestMethod]
  public async Task Get_ReturnsNotFoundIfThereIsNoMatchingDataFlow()
  {
    _dataFlowServiceMock.Setup(service => service.GetDataFlow(_dataFlow.Id)).ReturnsAsync(new ActionResult<DataFlow?>(_dataFlow));
    var newId = Guid.NewGuid();
    _dataFlowServiceMock.Setup(service => service.GetDataFlow(newId)).ReturnsAsync(new ActionResult<DataFlow?>((DataFlow?) null));
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object);

    var actionResult = await dataFlowController.GetDataFlow(_dataFlow.Id);
    Assert.IsInstanceOfType(actionResult, typeof(ActionResult<DataFlow>));
    
    var dataFlow = actionResult.Value;
    Assert.IsNotNull(dataFlow);
    Assert.AreSame(_dataFlow, dataFlow);

    actionResult = await dataFlowController.GetDataFlow(newId);
    Assert.IsInstanceOfType(actionResult, typeof(ActionResult<DataFlow>));
    
    dataFlow = actionResult.Value;
    Assert.IsNull(dataFlow);
  }

  [TestMethod]
  public async Task CanPutDataFlow()
  {
    _dataFlowServiceMock.Setup(service => service.PutDataFlow(_dataFlow)).Returns(new ActionResult<DataFlow>(_dataFlow));
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object);
    
    var actionResult = await dataFlowController.PutDataFlow(_dataFlow.Id, _dataFlow);
    Assert.IsInstanceOfType(actionResult, typeof(ActionResult<DataFlow>));

    var dataFlow = actionResult.Value;
    Assert.AreSame(_dataFlow, dataFlow);
  }

  [TestMethod]
  public async Task Put_ReturnsBadRequestIfIdsAreNotEqual()
  {
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object);

    var actionResult = await dataFlowController.PutDataFlow(Guid.NewGuid(), _dataFlow);

    Assert.IsNotNull(actionResult);
    Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestResult));
  }

  [TestMethod]
  public async Task Put_ReturnsNotFoundIfNoMatchingDataFlowExists()
  {
    _dataFlowServiceMock.Setup(service => service.SaveChangesAsync()).ThrowsAsync(new DbUpdateConcurrencyException());
    _dataFlowServiceMock.Setup(service => service.DataFlowExists(_dataFlow.Id)).Returns(false);

    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object);

    var actionResult = await dataFlowController.PutDataFlow(_dataFlow.Id, _dataFlow);

    Assert.IsNotNull(actionResult);
    Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
  }

  [TestMethod]
  [ExpectedException(typeof(DbUpdateConcurrencyException))]
  public async Task Put_ThrowsExceptionIfAMatchingDataFlowExistsButCouldNotBeOverriden()
  {
    _dataFlowServiceMock.Setup(service => service.SaveChangesAsync()).ThrowsAsync(new DbUpdateConcurrencyException());
    _dataFlowServiceMock.Setup(service => service.DataFlowExists(_dataFlow.Id)).Returns(true);
    
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object);

    await dataFlowController.PutDataFlow(_dataFlow.Id, _dataFlow);
  }

  [TestMethod]
  public async Task CanPostDataFlow()
  {
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object);

    var actionResult = await dataFlowController.PostDataFlow(_dataFlow);
    Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult));

    var createdAtAction = (actionResult.Result as CreatedAtActionResult);

    Assert.IsNotNull(createdAtAction?.Value);
    Assert.AreSame(_dataFlow, createdAtAction.Value);
  }

  [TestMethod]
  public async Task Post_ReturnsBadRequestIfDataFlowAlreadyExists()
  {
    _dataFlowServiceMock.Setup(service => service.DataFlowExists(_dataFlow.Id)).Returns(true);
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object);

    var actionResult = await dataFlowController.PostDataFlow(_dataFlow);
    Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestResult));
  }

  [TestMethod]
  public async Task CanDeleteDataFlow()
  {
    _dataFlowServiceMock.Setup(service => service.GetDataFlow(_dataFlow.Id)).ReturnsAsync(_dataFlow);

    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object);

    var actionResult = await dataFlowController.DeleteDataFlow(_dataFlow.Id);
    
    Assert.IsNotNull(actionResult);
    Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
  }

  [TestMethod]
  public async Task Delete_ReturnsNotFoundIfThereIsNoMatchingDataFlow()
  {
    _dataFlowServiceMock.Setup(service => service.GetDataFlow(_dataFlow.Id)).ReturnsAsync(_dataFlow);

    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object);

    var actionResult = await dataFlowController.DeleteDataFlow(Guid.NewGuid());
    
    Assert.IsNotNull(actionResult);
    Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
  }
}