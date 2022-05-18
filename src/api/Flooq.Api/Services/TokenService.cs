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

  public async Task<ActionResult<IEnumerable<Dictionary<string, string>>>> GetTokenIdsAndNamesByUserId(Guid userId)
  {
    var tokens = await _context.Tokens.Where(token => token.UserId.Equals(userId)).ToListAsync();
    var tokenIdsAndNames = new List<Dictionary<string, string>>();
    foreach (var token in tokens)
    {
      var idNameDict = new Dictionary<string, string>
      {
        {"Id", token.Id.ToString()!},
        {"Name", token.Name!}
      };
      tokenIdsAndNames.Add(idNameDict);
    }

    return new ActionResult<IEnumerable<Dictionary<string, string>>>(tokenIdsAndNames);
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

  public bool IsTokenOwnedByUser(Guid? tokenId, Guid? userId)
  {
    return _context.Tokens.Any(e => e.Id.Equals(tokenId) && e.UserId.Equals(userId));
  }

  public bool TokenExists(Guid? id)
  {
    return _context.Tokens.Any(e => e.Id.Equals(id));
  }

  public bool HasUserEquallyNamedToken(Guid? userId, string name)
  {
    return _context.Tokens.Any(e => e.Name == name && e.UserId.Equals(userId));
  }

  public void RemoveAllTokensByUserId(Guid? userId) {
    var tokensByUser = _context.Tokens.Where(d => d.UserId.Equals(userId)).ToArray();
    _context.Tokens.RemoveRange(tokensByUser);
  }
}