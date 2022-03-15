using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Flooq.Controllers;
using Flooq.Model;
using Flooq.Services;

namespace Flooq.Test;

[TestClass]
public class VersionControllerTest
{
  private Mock<IVersionService> serviceMock;

  [TestInitialize]
  public void setup()
  {
    serviceMock = new Mock<IVersionService>();
  }

  [TestMethod]
  public void CanCreateVersionController()
  {
    var mockVersionService = serviceMock.Object;
    VersionController versionController = new VersionController(mockVersionService);
    Assert.IsNotNull(versionController);
  }

  [TestMethod]
  public void CanGetLatestVersion()
  {
    var version = new Version();
    serviceMock.Setup(service => service.GetLatestVersion()).ReturnsAsync(version);

    var mockVersionService = serviceMock.Object;
    VersionController versionController = new VersionController(mockVersionService);

    Assert.AreSame(version, versionController.GetVersion().Result.Value);
  }
}