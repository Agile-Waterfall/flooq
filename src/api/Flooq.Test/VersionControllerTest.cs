using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Flooq.Controllers;
using Flooq.Services;
using Flooq.Model;

namespace Flooq.Test;

[TestClass]
public class VersionControllerTest
{
  private Mock<DbAccessService> serviceMock;

  [TestInitialize]
  public void setup()
  {
    serviceMock = new Mock<DbAccessService>();
  }

  [TestMethod]
  public void CanCreateVersionController()
  {
    var mockDbAccessService = serviceMock.Object;
    VersionController versionController = new VersionController(mockDbAccessService);
    Assert.IsNotNull(versionController);
  }

  [TestMethod]
  public void CanGetLatestVersion()
  {
    var versionMock = new Mock<Version>();
    Version mockVersion = versionMock.Object;
    serviceMock.Setup(service => service.GetLatestVersion()).ReturnsAsync(mockVersion);

    var mockDbAccessService = serviceMock.Object;
    VersionController versionController = new VersionController(mockDbAccessService);

    Assert.AreEqual(mockVersion, versionController.GetVersion().Result);
  }
}