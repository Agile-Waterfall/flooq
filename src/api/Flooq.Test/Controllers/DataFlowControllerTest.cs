using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Flooq.Api.Controllers;
using Flooq.Api.Models;
using Flooq.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Flooq.Test.Controllers;

[TestClass]
public class DataFlowControllerTest
{
  private readonly Mock<IDataFlowService> _serviceMock = new Mock<IDataFlowService>();
  
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
    var demoDataFlow = new DataFlow
    {
      Id = Guid.NewGuid(),
      Name = "Demo Flow",
      Status = "Active",
      LastEdited = DateTime.Now,
      Definition = "{}"
    };
    
    var dataFlows = new List<DataFlow>();
    dataFlows.Add(demoDataFlow);
    
    var actionResult = new ActionResult<IEnumerable<DataFlow>>(dataFlows);
    _serviceMock.Setup(service => service.GetDataFlows()).ReturnsAsync(actionResult);

    var mockDataFlowService = _serviceMock.Object;
    var dataFlowController = new DataFlowController(mockDataFlowService);
    
    Assert.AreSame(actionResult, dataFlowController.GetDataFlows().Result);
  }

  [TestMethod]
  public void CanGetDataFlow()
  {
    var demoDataFlow = new DataFlow
    {
      Id = Guid.NewGuid(),
      Name = "Demo Flow",
      Status = "Active",
      LastEdited = DateTime.Now,
      Definition = "{}"
    };

    _serviceMock.Setup(service => service.GetDataFlow(demoDataFlow.Id)).ReturnsAsync(demoDataFlow);

    var mockDataFlowService = _serviceMock.Object;
    var dataFlowController = new DataFlowController(mockDataFlowService);

    Assert.AreSame(demoDataFlow, dataFlowController.GetDataFlow(demoDataFlow.Id));
  }
}