using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Flooq.Api.Controllers;
using Flooq.Api.Domain;
using Flooq.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Flooq.Test.Controllers;

[TestClass]
public class DataFlowControllerTest
{
  private readonly FlooqContext _context = new FlooqContext(new DbContextOptionsBuilder<FlooqContext>().UseInMemoryDatabase("FlooqDatabase").Options);
  
  [TestMethod]
  public void CanGetDemoDataFlow()
  {
    var dataFlow = new DataFlow();
    DataFlowController dataFlowController = new DataFlowController(_context);

    Assert.IsNotNull(dataFlowController.GetDataFlows());
  }
}