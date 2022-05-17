using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Flooq.Api.Controllers;
using Flooq.Api.Models;
using Flooq.Api.Services;

namespace Flooq.Test.Controllers;

[TestClass]
public class VersionControllerTest
{
  private readonly Mock<IVersionService> _serviceMock = new ();

  [TestMethod]
  public void CanCreateVersionController()
  {
    var mockVersionService = _serviceMock.Object;
    VersionController versionController = new VersionController(mockVersionService);
    Assert.IsNotNull(versionController);
  }

  [TestMethod]
  public void CanGetLatestVersion()
  {
    var version = new Version();
    _serviceMock.Setup(service => service.GetLatestVersion()).ReturnsAsync(version);

    var mockVersionService = _serviceMock.Object;
    VersionController versionController = new VersionController(mockVersionService);

    Assert.AreSame(version, versionController.GetVersion().Result.Value);
  }

  [TestMethod]
  public void CannotGetVersion_WithoutVersionAvailable()
  {
    _serviceMock.Setup(service => service.GetLatestVersion()).ReturnsAsync((Version) null!);

    var mockVersionService = _serviceMock.Object;
    var versionController = new VersionController(mockVersionService);
    Assert.AreSame(null, versionController.GetVersion().Result.Value);
  }
}