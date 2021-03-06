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
  private static readonly Guid TestUserId = Guid.NewGuid();

  private readonly Token _token = new() 
  {
    Id = Guid.NewGuid(),
    Name = "Demo Token #1",
    UserId = TestUserId,
    LastEdited = DateTime.Now,
    Value = "TestToken"
  };

  private FlooqContext _context = null!;

  [TestInitialize]
  public async Task Setup()
  {
    var config = new ConfigurationManager();
    config.AddJsonFile("appsettings.Test.json");
    _context = new FlooqContext(new DbContextOptionsBuilder<FlooqContext>()
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
  public async Task CanGetTokenIdsAndNamesByUserId()
  {
    var tokenService = new TokenService(_context);
    var actionResult = await tokenService.GetTokenIdsAndNamesByUserId(TestUserId);
    
    Assert.AreEqual(0, actionResult.Value?.Count());

    _context.Tokens.Add(_token);
    await _context.SaveChangesAsync();
    
    actionResult = await tokenService.GetTokenIdsAndNamesByUserId(TestUserId);
    var receivedTokenIdsAndNames = actionResult.Value;

    Assert.AreEqual(1, receivedTokenIdsAndNames!.Count());
    Assert.IsTrue(receivedTokenIdsAndNames!.Any(
      e => e.Count == 2 && e.ContainsKey("Id") && e.ContainsKey("Name") && 
           e.ContainsValue(_token.Id.ToString()!) && e.ContainsValue(_token.Name!)));
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
    var actionResultTokenIdsAndNames = await tokenService.GetTokenIdsAndNamesByUserId(TestUserId);
    
    Assert.AreEqual(1, actionResultTokenIdsAndNames.Value?.Count());

    const string newName = "Changed Token";
    var newToken = new Token
    {
      Id = _token.Id,
      Name = newName,
      UserId = TestUserId,
      LastEdited = DateTime.Now,
      Value = "TestToken"
    };
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
  public async Task CanAddToken()
  {
    var tokenService = new TokenService(_context);

    Assert.AreEqual(0, _context.Tokens.Count());
    
    tokenService.AddToken(_token);
    await tokenService.SaveChangesAsync();
    
    var actionResult = await tokenService.GetTokenById(_token.Id);
    var token = actionResult.Value;
    
    Assert.IsNotNull(token);
    Assert.AreEqual(_token.Id, token.Id);
    Assert.AreEqual(1, _context.Tokens.Count());
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
  public async Task TestTokenExists()
  {
    var tokenService = new TokenService(_context);

    Assert.IsFalse(tokenService.TokenExists(_token.Id));

    tokenService.AddToken(_token);
    await tokenService.SaveChangesAsync();

    Assert.IsTrue(tokenService.TokenExists(_token.Id));
  }

  [TestMethod]
  public async Task TestIsTokenOwnedByUser()
  {
    var tokenService = new TokenService(_context);
    tokenService.AddToken(_token);
    await tokenService.SaveChangesAsync();
    
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
    await tokenService.SaveChangesAsync();
    
    Assert.IsFalse(tokenService.IsTokenOwnedByUser(newToken.Id, _token.UserId));
  }

  [TestMethod]
  public async Task TestHasUserEquallyNamedToken()
  {
    var tokenService = new TokenService(_context);
    
    Assert.IsFalse(tokenService.HasUserEquallyNamedToken(TestUserId, _token.Name!));
    
    tokenService.AddToken(_token);
    await tokenService.SaveChangesAsync();
    
    Assert.IsTrue(tokenService.HasUserEquallyNamedToken(TestUserId, _token.Name!));
  }

  [TestMethod]
  public async Task CanRemoveAllTokens()
  {
    var tokenService = new TokenService(_context);

    _context.Tokens.Add(_token);
    await _context.SaveChangesAsync();
    var token = await _context.Tokens.FindAsync(_token.Id);
    
    Assert.IsNotNull(token);
    Assert.AreEqual(1, _context.Tokens.Count());
    
    tokenService.RemoveAllTokensByUserId(TestUserId);
    await _context.SaveChangesAsync();
    
    var removedToken = await _context.Tokens.FindAsync(_token.Id);
    
    Assert.IsNull(removedToken);
    Assert.AreEqual(0, _context.Tokens.Count());
  }
}