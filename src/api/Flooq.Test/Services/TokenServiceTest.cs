using System;
using System.Linq;
using System.Threading.Tasks;
using Flooq.Api.Domain;
using Flooq.Api.Models;
using Flooq.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Flooq.Test.Services;

[TestClass]
public class TokenServiceTest
{
  private static readonly Guid TEST_USER_ID = Guid.NewGuid();

  private FlooqContext _context;
  private readonly Token _token = new() 
  {
    Id = Guid.NewGuid(),
    Name = "Demo Token #1",
    UserId = TEST_USER_ID,
    LastEdited = DateTime.Now,
    Value = "TestToken"
  };

  [TestInitialize]
  public async Task Setup()
  {
    var config = new ConfigurationManager();
    config.AddJsonFile("appsettings.Test.json");
    _context = new (new DbContextOptionsBuilder<FlooqContext>()
      .UseInMemoryDatabase(databaseName: "FlooqDatabase").Options, config);
    
    foreach (var token in _context.Tokens) _context.Tokens.Remove(token);
    await _context.SaveChangesAsync();
  }
  
  [TestMethod]
  public void CanCreateTokenService()
  {
    Assert.IsNotNull(_context.Tokens);
    var dataFlowService = new DataFlowService(_context);
    Assert.IsNotNull(dataFlowService);
  }

  [TestMethod]
  public async Task CanGetTokenNamesByUserId()
  {
    var tokenService = new TokenService(_context);
    var actionResult = await tokenService.GetTokenNamesByUserId(TEST_USER_ID);
    
    Assert.AreEqual(0, actionResult.Value?.Count());

    _context.Tokens.Add(_token);
    await _context.SaveChangesAsync();
    actionResult = await tokenService.GetTokenNamesByUserId(TEST_USER_ID);
    var receivedTokenNames = actionResult.Value;
    
    Assert.AreEqual(1, receivedTokenNames?.Count());
    Assert.IsTrue(receivedTokenNames.Contains(_token.Name));
  }

  [TestMethod]
  public async Task CanGetTokenById()
  {
    var tokenService = new TokenService(_context);
    _context.Tokens.Add(_token);
    await _context.SaveChangesAsync();

    var actionResult = await tokenService.GetTokenById(_token.Id);
    var dataFlow = actionResult.Value;
    
    Assert.AreSame(_token, dataFlow);
  }

  [TestMethod]
  public async Task CanPutToken()
  {
    var tokenService = new TokenService(_context);

    _context.Tokens.Add(_token);
    await tokenService.SaveChangesAsync();
    var actionResultTokenNames = await tokenService.GetTokenNamesByUserId(TEST_USER_ID);
    
    Assert.AreEqual(1, actionResultTokenNames.Value?.Count());

    const string newName = "Changed Token";
    var newToken = new Token
    {
      Id = _token.Id,
      Name = newName,
      UserId = TEST_USER_ID,
      LastEdited = DateTime.Now,
      Value = "TestToken"
    };
    // Need to detach the tracked instance _dataFlow. Don't know where the tracking started.
    // https://stackoverflow.com/questions/62253837/the-instance-of-entity-type-cannot-be-tracked-because-another-instance-with-the
    _context.Entry(_token).State = EntityState.Detached;
    
    var actionResult = tokenService.PutToken(newToken);
    Assert.AreEqual(EntityState.Modified, _context.Entry(newToken).State);

    await _context.SaveChangesAsync();
    Assert.AreEqual(EntityState.Unchanged, _context.Entry(newToken).State);
    
    var token = actionResult.Value;
    Assert.AreNotEqual(_token, token);
    Assert.AreEqual(newName, token?.Name);
  }

  [TestMethod]
  public async Task CanAddTokenAndSaveChangesAsync()
  {
    var tokenService = new TokenService(_context);

    var actionResult = await tokenService.GetTokenById(_token.Id);
    var token = actionResult.Value;
    
    Assert.IsNull(token);
    
    tokenService.AddToken(_token);
    await tokenService.SaveChangesAsync();
    actionResult = await tokenService.GetTokenById(_token.Id);
    token = actionResult.Value;
    
    Assert.IsNotNull(token);
    Assert.AreEqual(_token.Id, token.Id);
  }

  [TestMethod]
  public async Task CanRemoveToken()
  {
    var tokenService = new TokenService(_context);

    _context.Tokens.Add(_token);
    await _context.SaveChangesAsync();
    var token = await _context.Tokens.FindAsync(_token.Id);
    
    Assert.IsNotNull(token);
    Assert.AreEqual(1, _context.Tokens.Count());
    
    var removedToken = tokenService.RemoveToken(_token).Entity;
    await _context.SaveChangesAsync();
    
    Assert.IsNotNull(removedToken);
    Assert.AreEqual(_token.Id, token.Id);
    Assert.AreEqual(0, _context.Tokens.Count());
  }

  [TestMethod]
  public void TestTokenExists()
  {
    var tokenService = new TokenService(_context);

    Assert.IsFalse(tokenService.TokenExists(_token.Id));

    tokenService.AddToken(_token);
    tokenService.SaveChangesAsync();

    Assert.IsTrue(tokenService.TokenExists(_token.Id));
  }

  [TestMethod]
  public void TestIsTokenOwnedByUser()
  {
    var tokenService = new TokenService(_context);
    
    tokenService.AddToken(_token);
    tokenService.SaveChangesAsync();
    
    Assert.IsTrue(tokenService.IsTokenOwnedByUser(_token.Id, _token.UserId));
    
    var newToken = new Token
    {
      Id = Guid.NewGuid(),
      Name = "other user",
      UserId = Guid.NewGuid(),
      LastEdited = DateTime.Now,
      Value = "TestToken"
    };
    tokenService.AddToken(newToken);
    tokenService.SaveChangesAsync();
    Assert.IsFalse(tokenService.IsTokenOwnedByUser(newToken.Id, _token.UserId));
  }

  [TestMethod]
  public void TestHasUserEquallyNamedToken()
  {
    var tokenService = new TokenService(_context);
    Assert.IsFalse(tokenService.HasUserEquallyNamedToken(TEST_USER_ID, _token.Name!));
    
    tokenService.AddToken(_token);
    tokenService.SaveChangesAsync();
    Assert.IsTrue(tokenService.HasUserEquallyNamedToken(TEST_USER_ID, _token.Name!));
  }
}