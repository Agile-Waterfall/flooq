using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Flooq.Api.Controllers;
using Flooq.Api.Model;
using Flooq.Api.Services;

namespace Flooq.Test.Controllers;

[TestClass]
public class VersionControllerTest
{
  private Mock<IVersionService> serviceMock;

  [TestInitialize]
  public void Setup()
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

  [TestMethod]
  public void CannotGetVersion_WithoutVersionAvailable()
  {
    serviceMock.Setup(service => service.GetLatestVersion()).ReturnsAsync((Version)null);

    var mockVersionService = serviceMock.Object;
    var versionController = new VersionController(mockVersionService);
    Assert.AreSame(null, versionController.GetVersion().Result.Value);
  }
}