using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Flooq.Api.Controllers;
using Flooq.Api.Services;

namespace Flooq.Test.Controllers;

[TestClass]
public class DataFlowControllerTest
{
  private readonly Mock<IDataFlowService> _serviceMock = new Mock<IDataFlowService>();
  
  [TestMethod]
  public void CanCreateDataFlowController()
  {
    var mockDataFlowService = _serviceMock.Object;
    DataFlowController dataFlowController = new DataFlowController(mockDataFlowService);
    Assert.IsNotNull(dataFlowController);
  }
}