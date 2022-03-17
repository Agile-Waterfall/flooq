using Microsoft.VisualStudio.TestTools.UnitTesting;
using Flooq.Controllers;

namespace Flooq.Test;

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