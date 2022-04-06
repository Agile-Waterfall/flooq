using Microsoft.VisualStudio.TestTools.UnitTesting;
using Flooq.Api.Controllers;

namespace Flooq.Test.Controllers;

[TestClass]
public class StatusControllerTest
{
  [TestMethod]
  public void CanCreateVersionController()
  {
    StatusController statusController = new StatusController();
    Assert.AreEqual("running", statusController.GetStatus());
  }
}