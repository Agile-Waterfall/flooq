using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Flooq.Domain;
using Flooq.Model;
using Flooq.Services;

namespace Flooq.Test;

[TestClass]
public class VersionServiceTest
{
  private FlooqContext context;

  [TestInitialize]
  public void setup()
  {
    var options = new DbContextOptionsBuilder<FlooqContext>().UseInMemoryDatabase(databaseName: "FlooqDatabase").Options;
    context = new FlooqContext(options);
  }

  [TestMethod]
  public void CanCreateVersionService()
  {
    VersionService versionService = new VersionService(context);
    Assert.IsNotNull(versionService);
  }

  [TestMethod]
  public void CanGetVersion()
  {
    var version = new Version() { Tag = "0.0.1", Name = "Test 1", Notes = "Test version" };
    context.Versions.Add(version);
    context.SaveChanges();
    VersionService versionService = new VersionService(context);
    Assert.AreEqual(version.Tag, versionService.GetLatestVersion().Result.Value.Tag);
  }

  [TestMethod]
  public void CanGetLatestVersion()
  {
    var versions = new Version[] { new Version() { Tag = "0.0.2", Name = "Test 2", Notes = "Test version" },
                                   new Version() { Tag = "0.0.3", Name = "Test 3", Notes = "Test version" },
                                   new Version() { Tag = "0.0.4", Name = "Test 4", Notes = "Test version" }};

    context.Versions.AddRange(versions);
    context.SaveChanges();

    VersionService versionService = new VersionService(context);
    Assert.AreEqual(versions[2].Tag, versionService.GetLatestVersion().Result.Value.Tag);
  }
}