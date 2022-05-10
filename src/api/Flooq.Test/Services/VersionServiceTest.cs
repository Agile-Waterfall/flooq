using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Flooq.Api.Domain;
using Flooq.Api.Models;
using Flooq.Api.Services;
using Microsoft.Extensions.Configuration;

namespace Flooq.Test.Services;

[TestClass]
public class VersionServiceTest
{
  private readonly FlooqContext _context = new (new DbContextOptionsBuilder<FlooqContext>()
    .UseInMemoryDatabase(databaseName: "FlooqDatabase").Options, new ConfigurationManager());

  [TestMethod]
  public void CanCreateVersionService()
  {
    var versionService = new VersionService(_context);
    Assert.IsNotNull(versionService);
  }

  [TestMethod]
  public void CanGetVersion()
  {
    var version = new Version() { Tag = "0.0.1", Name = "Test 1", Notes = "Test version" };
    _context.Versions.Add(version);
    _context.SaveChanges();
    var versionService = new VersionService(_context);
    Assert.AreEqual(version.Tag, versionService.GetLatestVersion().Result.Value?.Tag);
  }

  [TestMethod]
  public void CanGetLatestVersion()
  {
    var versions = new [] { new Version() { Tag = "0.0.2", Name = "Test 2", Notes = "Test version" },
                                   new Version() { Tag = "0.0.3", Name = "Test 3", Notes = "Test version" },
                                   new Version() { Tag = "0.0.4", Name = "Test 4", Notes = "Test version" }};

    _context.Versions.AddRange(versions);
    _context.SaveChanges();

    var versionService = new VersionService(_context);
    Assert.AreEqual(versions[2].Tag, versionService.GetLatestVersion().Result.Value?.Tag);
  }
}