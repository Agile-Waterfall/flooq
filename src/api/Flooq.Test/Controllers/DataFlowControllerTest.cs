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
  private readonly Mock<IDataFlowService> _serviceMock = new Mock<IDataFlowService>();
  private readonly DataFlow _dataFlow = new DataFlow
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
    var mockDataFlowService = _serviceMock.Object;
    var dataFlowController = new DataFlowController(mockDataFlowService);
    Assert.IsNotNull(dataFlowController);
  }

  [TestMethod]
  public void CanGetDataFlows()
  {
    var dataFlows = new List<DataFlow>();
    dataFlows.Add(_dataFlow);
    
    var actionResult = new ActionResult<IEnumerable<DataFlow>>(dataFlows);
    _serviceMock.Setup(service => service.GetDataFlows()).ReturnsAsync(actionResult);

    var dataFlowController = new DataFlowController(_serviceMock.Object);
    
    Assert.AreSame(actionResult, dataFlowController.GetDataFlows().Result);
  }

  [TestMethod]
  public void CanGetDataFlow()
  {
    _serviceMock.Setup(service => service.GetDataFlow(_dataFlow.Id)).ReturnsAsync(_dataFlow);

    var dataFlowController = new DataFlowController(_serviceMock.Object);

    Assert.AreSame(_dataFlow, dataFlowController.GetDataFlow(_dataFlow.Id).Result.Value);
  }

  [TestMethod]
  public async Task Get_ReturnsNotFoundIfThereIsNoMatchingDataFlow()
  {
    _serviceMock.Setup(service => service.GetDataFlow(_dataFlow.Id)).ReturnsAsync(new ActionResult<DataFlow>(_dataFlow));
    
    var dataFlowController = new DataFlowController(_serviceMock.Object);

    ActionResult<DataFlow> result = await dataFlowController.GetDataFlow(Guid.NewGuid());
    
    Assert.IsNotNull(result.Result);
    Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
  }

  [TestMethod]
  public void CanPutDataFlow()
  {
    var dataFlowController = new DataFlowController(_serviceMock.Object);

    var result = dataFlowController.PutDataFlow(_dataFlow.Id, _dataFlow);
    
    Assert.IsNotNull(result.Result);
    Assert.IsInstanceOfType(result.Result, typeof(NoContentResult));
  }

  [TestMethod]
  public async Task Put_ReturnsBadRequestIfIdsAreNotEqual()
  {
    var dataFlowController = new DataFlowController(_serviceMock.Object);

    var result = await dataFlowController.PutDataFlow(Guid.NewGuid(), _dataFlow);

    Assert.IsNotNull(result);
    Assert.IsInstanceOfType(result, typeof(BadRequestResult));
  }

  [TestMethod]
  public async Task Put_ReturnsNotFoundIfNoMatchingDataFlowExists()
  {
    _serviceMock.Setup(service => service.SaveChangesAsync()).ThrowsAsync(new DbUpdateConcurrencyException());
    _serviceMock.Setup(service => service.DataFlowExists(_dataFlow.Id)).Returns(false);

    var dataFlowController = new DataFlowController(_serviceMock.Object);

    var result = await dataFlowController.PutDataFlow(_dataFlow.Id, _dataFlow);

    Assert.IsNotNull(result);
    Assert.IsInstanceOfType(result, typeof(NotFoundResult));
  }

  [TestMethod]
  public async Task CanPostDataFlow()
  {
    var dataFlowController = new DataFlowController(_serviceMock.Object);

    var result = await dataFlowController.PostDataFlow(_dataFlow);
    Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));

    var createdAtAction = (result.Result as CreatedAtActionResult);

    Assert.IsNotNull(createdAtAction?.Value);
    Assert.AreSame(_dataFlow, createdAtAction.Value);
  }

  [TestMethod]
  public void CanDeleteDataFlow()
  {
    _serviceMock.Setup(service => service.GetDataFlow(_dataFlow.Id)).ReturnsAsync(_dataFlow);

    var dataFlowController = new DataFlowController(_serviceMock.Object);

    var result = dataFlowController.DeleteDataFlow(_dataFlow.Id);
    
    Assert.IsNotNull(result.Result);
    Assert.IsInstanceOfType(result.Result, typeof(NoContentResult));
  }

  [TestMethod]
  public void Delete_ReturnsNotFoundIfThereIsNoMatchingDataFlow()
  {
    _serviceMock.Setup(service => service.GetDataFlow(_dataFlow.Id)).ReturnsAsync(_dataFlow);

    var dataFlowController = new DataFlowController(_serviceMock.Object);

    var result = dataFlowController.DeleteDataFlow(Guid.NewGuid());
    
    Assert.IsNotNull(result);
    Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
  }
}