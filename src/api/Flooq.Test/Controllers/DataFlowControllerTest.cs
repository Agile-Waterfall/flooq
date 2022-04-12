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
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object);
    Assert.IsNotNull(dataFlowController);
  }

  [TestMethod]
  public void CanGetDataFlows()
  {
    var dataFlows = new List<DataFlow> {_dataFlow};
    
    var actionResult = new ActionResult<IEnumerable<DataFlow>>(dataFlows);
    _dataFlowServiceMock.Setup(service => service.GetDataFlows()).ReturnsAsync(actionResult);

    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object);
    
    Assert.AreSame(actionResult, dataFlowController.GetDataFlows().Result);
  }

  [TestMethod]
  public void CanGetDataFlow()
  {
    _dataFlowServiceMock.Setup(service => service.GetDataFlow(_dataFlow.Id)).ReturnsAsync(_dataFlow);

    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object);

    Assert.AreSame(_dataFlow, dataFlowController.GetDataFlow(_dataFlow.Id).Result.Value);
  }

  [TestMethod]
  public async Task Get_ReturnsNotFoundIfThereIsNoMatchingDataFlow()
  {
    _dataFlowServiceMock.Setup(service => service.GetDataFlow(_dataFlow.Id)).ReturnsAsync(new ActionResult<DataFlow>(_dataFlow));
    
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object);

    ActionResult<DataFlow> result = await dataFlowController.GetDataFlow(Guid.NewGuid());
    
    Assert.IsNotNull(result.Result);
    Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
  }

  [TestMethod]
  public void CanPutDataFlow()
  {
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object);

    var result = dataFlowController.PutDataFlow(_dataFlow.Id, _dataFlow);
    
    Assert.IsNotNull(result.Result);
    Assert.IsInstanceOfType(result.Result, typeof(NoContentResult));
  }

  [TestMethod]
  public async Task Put_ReturnsBadRequestIfIdsAreNotEqual()
  {
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object);

    var result = await dataFlowController.PutDataFlow(Guid.NewGuid(), _dataFlow);

    Assert.IsNotNull(result);
    Assert.IsInstanceOfType(result, typeof(BadRequestResult));
  }

  [TestMethod]
  public async Task Put_ReturnsNotFoundIfNoMatchingDataFlowExists()
  {
    _dataFlowServiceMock.Setup(service => service.SaveChangesAsync()).ThrowsAsync(new DbUpdateConcurrencyException());
    _dataFlowServiceMock.Setup(service => service.DataFlowExists(_dataFlow.Id)).Returns(false);

    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object);

    var result = await dataFlowController.PutDataFlow(_dataFlow.Id, _dataFlow);

    Assert.IsNotNull(result);
    Assert.IsInstanceOfType(result, typeof(NotFoundResult));
  }

  [TestMethod]
  public async Task CanPostDataFlow()
  {
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object);

    var result = await dataFlowController.PostDataFlow(_dataFlow);
    Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));

    var createdAtAction = (result.Result as CreatedAtActionResult);

    Assert.IsNotNull(createdAtAction?.Value);
    Assert.AreSame(_dataFlow, createdAtAction.Value);
  }

  [TestMethod]
  public void CanDeleteDataFlow()
  {
    _dataFlowServiceMock.Setup(service => service.GetDataFlow(_dataFlow.Id)).ReturnsAsync(_dataFlow);

    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object);

    var result = dataFlowController.DeleteDataFlow(_dataFlow.Id);
    
    Assert.IsNotNull(result.Result);
    Assert.IsInstanceOfType(result.Result, typeof(NoContentResult));
  }

  [TestMethod]
  public void Delete_ReturnsNotFoundIfThereIsNoMatchingDataFlow()
  {
    _dataFlowServiceMock.Setup(service => service.GetDataFlow(_dataFlow.Id)).ReturnsAsync(_dataFlow);

    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object);

    var result = dataFlowController.DeleteDataFlow(Guid.NewGuid());
    
    Assert.IsNotNull(result);
    Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
  }
}