using Flooq.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Flooq.Api.Services;

public interface ITokenService
{
  Task<ActionResult<IEnumerable<string>>> GetTokenNamesByUserId(Guid userId);

  Task<ActionResult<Token?>> GetTokenById(Guid? tokenId);

  ActionResult<Token> PutToken(Token token);

  Task<int> SaveChangesAsync();

  EntityEntry<Token> AddToken(Token token);

  EntityEntry<Token> RemoveToken(Token token);

  bool IsTokenOwnedByUser(Guid? userId, string name);

  bool TokenExists(Guid? id);
}