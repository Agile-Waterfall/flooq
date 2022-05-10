using Flooq.Api.Domain;
using Flooq.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Flooq.Api.Services;

public class TokenService : ITokenService
{
  private readonly FlooqContext _context;

  public TokenService(FlooqContext context)
  {
    _context = context;
  }

  public async Task<ActionResult<IEnumerable<string>>> GetTokenNamesByUserId(Guid userId)
  {
    var tokens = await _context.Tokens.Where(token => token.UserId.Equals(userId)).ToListAsync();
    var tokenNames = new List<string>();
    foreach (var token in tokens)
    {
      tokenNames.Add(token.Name!);
    }

    return new ActionResult<IEnumerable<string>>(tokenNames);
  }

  public async Task<ActionResult<Token?>> GetTokenById(Guid? tokenId)
  {
    var token = await _context.Tokens.FindAsync(tokenId);
    return new ActionResult<Token?>(token);
  }

  public ActionResult<Token> PutToken(Token token)
  {
    _context.Entry(token).State = EntityState.Modified;
    return new ActionResult<Token>(token);
  }

  public async Task<int> SaveChangesAsync()
  {
    return await _context.SaveChangesAsync();
  }

  public EntityEntry<Token> AddToken(Token token)
  {
    return _context.Tokens.Add(token);
  }

  public EntityEntry<Token> RemoveToken(Token token)
  {
    return _context.Tokens.Remove(token);
  }

  public bool IsTokenOwnedByUser(Guid? userId, string name)
  {
    return _context.Tokens.Any(e => e.UserId.Equals(userId) && e.Name == name);
  }

  public bool TokenExists(Guid? id)
  {
    return _context.Tokens.Any(e => e.Id.Equals(id));
  }
}