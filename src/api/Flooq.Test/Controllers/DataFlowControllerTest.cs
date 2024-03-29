﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Flooq.Api.Controllers;
using Flooq.Api.Metrics.Services;
using Flooq.Api.Models;
using Flooq.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flooq.Test.Controllers;

[TestClass]
public class DataFlowControllerTest
{
  private static readonly Guid TestUserId = Guid.NewGuid();

  private readonly DataFlow _dataFlow = new() 
  {
    Id = Guid.NewGuid(),
    Name = "Demo Flow",
    Status = "Active",
    LastEdited = DateTime.Now,
    Definition = "{}",
    UserId = TestUserId
  };
  private readonly ClaimsPrincipal _user = new(new ClaimsIdentity(new[]
  {
    new Claim(ClaimTypes.NameIdentifier, TestUserId.ToString()),
  }, "mock"));
  
  private readonly Mock<IDataFlowService> _dataFlowServiceMock = new();
  private readonly Mock<ILinearizedGraphService> _graphServiceMock = new();
  private readonly Mock<IDataFlowMetricsService> _dataFlowMetricsServiceMock = new();
  
  [TestMethod]
  public void CanCreateDataFlowController()
  {
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object, _dataFlowMetricsServiceMock.Object);
    
    Assert.IsNotNull(dataFlowController);
  }
  
  [TestMethod]
  public async Task CanGetDataFlowsByUser_Zero()
  {
    var dataFlows = new List<DataFlow>(); 
    var actionResult = new ActionResult<IEnumerable<DataFlow>>(dataFlows);
    _dataFlowServiceMock.Setup(service => service.GetDataFlowsByUserId(TestUserId)).ReturnsAsync(actionResult);
    
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object, _dataFlowMetricsServiceMock.Object);
    dataFlowController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };

    var actionResultReceived = await dataFlowController.GetDataFlowsByUser();
    
    Assert.AreSame(actionResult, actionResultReceived);
    Assert.IsNull(actionResultReceived.Value?.GetEnumerator().Current);
  }

  [TestMethod]
  public async Task CanGetDataFlowsByUser_One()
  {
    var dataFlows = new List<DataFlow> {_dataFlow};
    var actionResult = new ActionResult<IEnumerable<DataFlow>>(dataFlows);
    _dataFlowServiceMock.Setup(service => service.GetDataFlowsByUserId(TestUserId)).ReturnsAsync(actionResult);
    
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object, _dataFlowMetricsServiceMock.Object);
    dataFlowController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };

    var actionResultReceived = await dataFlowController.GetDataFlowsByUser();
    
    Assert.AreSame(actionResult, actionResultReceived);
  }

  [TestMethod]
  public async Task CanGetDataFlowsByUser_Multiple()
  {
    DataFlow dataFlow2 = new()
    {
      Id = Guid.NewGuid(),
      Name = "Demo Flow 2",
      Status = "Active",
      LastEdited = DateTime.Now,
      Definition = "{}",
      UserId = TestUserId
    };
    var dataFlows = new List<DataFlow> {_dataFlow, dataFlow2};
    var actionResult = new ActionResult<IEnumerable<DataFlow>>(dataFlows);
    _dataFlowServiceMock.Setup(service => service.GetDataFlowsByUserId(TestUserId)).ReturnsAsync(actionResult);
    
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object, _dataFlowMetricsServiceMock.Object);
    dataFlowController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };

    var actionResultReceived = await dataFlowController.GetDataFlowsByUser();
    
    Assert.AreSame(actionResult, actionResultReceived);
  }

  [TestMethod]
  public async Task CanGetDataFlow()
  {
    _dataFlowServiceMock.Setup(service => service.GetDataFlowById(_dataFlow.Id)).ReturnsAsync(_dataFlow);
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object, _dataFlowMetricsServiceMock.Object);

    var actionResultReceived = await dataFlowController.GetDataFlow(_dataFlow.Id);
    var dataFlowReceived = actionResultReceived.Value;
    
    Assert.AreSame(_dataFlow, dataFlowReceived);
  }

  [TestMethod]
  public async Task Get_ReturnsNotFoundIfThereIsNoMatchingDataFlow()
  {
    var newId = Guid.NewGuid();
    _dataFlowServiceMock.Setup(service => service.GetDataFlowById(newId)).ReturnsAsync(new ActionResult<DataFlow?>((DataFlow?) null));
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object, _dataFlowMetricsServiceMock.Object);

    var actionResult = await dataFlowController.GetDataFlow(newId);
    
    Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
  }
  
  [TestMethod]
  public async Task CanGetDataFlowByUser()
  {
    _dataFlowServiceMock.Setup(service => service.GetDataFlowByIdByUserId(_dataFlow.Id, TestUserId)).ReturnsAsync(_dataFlow);
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object, _dataFlowMetricsServiceMock.Object);
    dataFlowController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };

    var actionResultReceived = await dataFlowController.GetDataFlowByUser(_dataFlow.Id);
    var dataFlowReceived = actionResultReceived.Value;
    
    Assert.AreSame(_dataFlow, dataFlowReceived);
  }

  [TestMethod]
  public async Task GetByUser_ReturnsNotFoundIfThereIsNoMatchingDataFlow()
  {
    var newId = Guid.NewGuid();
    _dataFlowServiceMock.Setup(service => service.GetDataFlowByIdByUserId(newId, TestUserId)).ReturnsAsync(new ActionResult<DataFlow?>((DataFlow?) null));
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object, _dataFlowMetricsServiceMock.Object);
    dataFlowController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };

    var actionResult = await dataFlowController.GetDataFlowByUser(newId);
    
    Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
  }

  [TestMethod]
  public async Task CanPutDataFlow()
  {
    _dataFlowServiceMock.Setup(service => service.GetDataFlowById(_dataFlow.Id)).ReturnsAsync(_dataFlow);
    _dataFlowServiceMock.Setup(service => service.IsDataFlowOwnedByUser(_dataFlow.Id, _dataFlow.UserId)).Returns(true);
    _dataFlowServiceMock.Setup(service => service.PutDataFlow(_dataFlow)).Returns(new ActionResult<DataFlow>(_dataFlow));
    
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object, _dataFlowMetricsServiceMock.Object);
    dataFlowController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };
    
    var actionResult = await dataFlowController.PutDataFlow(_dataFlow.Id, _dataFlow);
    
    Assert.IsInstanceOfType(actionResult, typeof(ActionResult<DataFlow>));
    Assert.AreSame(_dataFlow, actionResult.Value);
  }

  [TestMethod]
  public async Task Put_ReturnsBadRequestIfIdsAreNotEqual()
  {
    _dataFlowServiceMock.Setup(service => service.GetDataFlowById(_dataFlow.Id)).ReturnsAsync(_dataFlow);
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object, _dataFlowMetricsServiceMock.Object);
    dataFlowController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };

    var actionResult = await dataFlowController.PutDataFlow(Guid.NewGuid(), _dataFlow);

    Assert.IsNotNull(actionResult);
    Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestResult));
  }

  [TestMethod]
  public async Task Put_ReturnsUnauthorizedIfDataFlowIsNotOwnedByUser()
  {
    _dataFlowServiceMock.Setup(service => service.IsDataFlowOwnedByUser(_dataFlow.Id, _dataFlow.UserId)).Returns(false);
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object, _dataFlowMetricsServiceMock.Object);
    dataFlowController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };

    var actionResult = await dataFlowController.PutDataFlow(_dataFlow.Id, _dataFlow);

    Assert.IsNotNull(actionResult);
    Assert.IsInstanceOfType(actionResult.Result, typeof(UnauthorizedResult));
  }

  [TestMethod]
  [ExpectedException(typeof(DbUpdateConcurrencyException))]
  public async Task Put_ThrowsExceptionIfAMatchingDataFlowExistsButCouldNotBeOverriden()
  {
    _dataFlowServiceMock.Setup(service => service.GetDataFlowById(_dataFlow.Id)).ReturnsAsync(_dataFlow);
    _dataFlowServiceMock.Setup(service => service.IsDataFlowOwnedByUser(_dataFlow.Id, _dataFlow.UserId)).Returns(true);
    _dataFlowServiceMock.Setup(service => service.SaveChangesAsync()).ThrowsAsync(new DbUpdateConcurrencyException());
    _dataFlowServiceMock.Setup(service => service.DataFlowExists(_dataFlow.Id)).Returns(true);
    
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object, _dataFlowMetricsServiceMock.Object);
    dataFlowController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };

    await dataFlowController.PutDataFlow(_dataFlow.Id, _dataFlow);
  }

  [TestMethod]
  public async Task Put_DeletesMatchingLinearizedGraph()
  {
    Assert.IsNotNull(_dataFlow.Id);
    
    LinearizedGraph graph = new()
    {
      Id = _dataFlow.Id.Value,
      Graph = ""
    };
    
    _dataFlowServiceMock.Setup(service => service.GetDataFlowById(_dataFlow.Id)).ReturnsAsync(_dataFlow);
    _dataFlowServiceMock.Setup(service => service.IsDataFlowOwnedByUser(_dataFlow.Id, _dataFlow.UserId)).Returns(true);
    _dataFlowServiceMock.Setup(service => service.PutDataFlow(_dataFlow)).Returns(new ActionResult<DataFlow>(_dataFlow));
    _graphServiceMock.Setup(service => service.GetGraph(_dataFlow.Id.Value))
      .ReturnsAsync(new ActionResult<LinearizedGraph?>(graph));
    
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object, _dataFlowMetricsServiceMock.Object);
    dataFlowController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };

    var actionResult = await dataFlowController.PutDataFlow(_dataFlow.Id, _dataFlow);
    
    Assert.IsInstanceOfType(actionResult, typeof(ActionResult<DataFlow>));
    Assert.AreSame(_dataFlow, actionResult.Value);
    
    _graphServiceMock.Verify(service => service.RemoveGraph(graph), Times.Once);
    _graphServiceMock.Verify(service => service.SaveChangesAsync(), Times.Once);
  }

  [TestMethod]
  public async Task CanPostDataFlow()
  {
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object, _dataFlowMetricsServiceMock.Object);
    dataFlowController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };

    var actionResult = await dataFlowController.PostDataFlow(_dataFlow);
    
    Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult));

    var createdAtAction = actionResult.Result as CreatedAtActionResult;

    Assert.IsNotNull(createdAtAction?.Value);
    Assert.AreSame(_dataFlow, createdAtAction.Value);
    Assert.AreEqual(_dataFlow.UserId, TestUserId);
  }

  [TestMethod]
  public async Task Post_ReturnsConflictIfDataFlowAlreadyExists()
  {
    _dataFlowServiceMock.Setup(service => service.DataFlowExists(_dataFlow.Id)).Returns(true);
    _dataFlowServiceMock.Setup(service => service.SaveChangesAsync()).ThrowsAsync(new ArgumentException());
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object, _dataFlowMetricsServiceMock.Object)
    {
      ControllerContext = new ControllerContext
      {
        HttpContext = new DefaultHttpContext { User = _user }
      }
    };

    var actionResult = await dataFlowController.PostDataFlow(_dataFlow);
    
    Assert.IsInstanceOfType(actionResult.Result, typeof(ConflictResult));
  }

  [TestMethod]
  [ExpectedException(typeof(DbUpdateException))]
  public async Task Post_ThrowsExceptionIfDataFlowDoesNotExistButCannotBePosted() 
  {
    _dataFlowServiceMock.Setup(service => service.SaveChangesAsync()).ThrowsAsync(new DbUpdateException());
    _dataFlowServiceMock.Setup(service => service.DataFlowExists(_dataFlow.Id)).Returns(false);
    
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object, _dataFlowMetricsServiceMock.Object);
    dataFlowController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };

    await dataFlowController.PostDataFlow(_dataFlow);
  }

  [TestMethod]
  public async Task CanDeleteDataFlow()
  {
    _dataFlowServiceMock.Setup(service => service.GetDataFlowById(_dataFlow.Id)).ReturnsAsync(_dataFlow);
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object, _dataFlowMetricsServiceMock.Object);

    var actionResult = await dataFlowController.DeleteDataFlow(_dataFlow.Id);
    
    Assert.IsNotNull(actionResult);
    Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
  }

  [TestMethod]
  public async Task Delete_ReturnsNotFoundIfThereIsNoMatchingDataFlow()
  {
    _dataFlowServiceMock.Setup(service => service.GetDataFlowById(_dataFlow.Id)).ReturnsAsync(_dataFlow);
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object, _dataFlowMetricsServiceMock.Object);

    var actionResult = await dataFlowController.DeleteDataFlow(Guid.NewGuid());
    
    Assert.IsNotNull(actionResult);
    Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
  }

  [TestMethod]
  public async Task CanDeleteAllDataFlows()
  {
    _dataFlowServiceMock.Setup(service => service.GetDataFlowById(_dataFlow.Id)).ReturnsAsync(_dataFlow);
    var dataFlowController = new DataFlowController(_dataFlowServiceMock.Object, _graphServiceMock.Object, _dataFlowMetricsServiceMock.Object);
    dataFlowController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };

    var actionResult = await dataFlowController.DeleteAllDataFlows();
    
    Assert.IsNotNull(actionResult);
    Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
  }
}