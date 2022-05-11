using Flooq.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Flooq.Api.Services;

/// <summary>
/// Provides for querying Flooq <see cref="Token"/>s from the Flooq database.
/// </summary>
public interface ITokenService
{
  /// <summary>
  /// Queries all <see cref="Token"/> names for the user having the given id from the Flooq database.
  /// </summary>
  /// <returns>All <see cref="Token"/> names.</returns>
  /// <param name="userId">The id of the user.</param>
  Task<ActionResult<IEnumerable<string>>> GetTokenNamesByUserId(Guid userId);

  /// <summary>
  /// Finds a specific <see cref="Token"/> in the Flooq database according to a unique identification.
  /// </summary>
  /// <returns>The identified <see cref="Token"/>.</returns>
  /// <param name="id">The value which is used for identifying the <see cref="Token"/>.</param>
  Task<ActionResult<Token?>> GetTokenById(Guid? tokenId);

  /// <summary>
  /// The existing <see cref="Token"/> entry in the database will be overwritten by the new <see cref="Token"/> when a save operation on the
  /// associated <see cref="DbContext"/> is called afterwards.
  /// </summary>
  /// <returns>The updated <see cref="Token"/></returns>
  /// <param name="token">The new <see cref="Token"/>.</param>
  ActionResult<Token> PutToken(Token token);

  /// <summary>
  /// Saves all previously made changes in this <see cref="DbContext"/> to the database.
  /// </summary>
  /// <returns>
  /// A <see cref="Task"/> that represents the asynchronous save operation.
  /// The <see cref="Task"/> result contains the number of state entries written to the database.
  /// </returns>
  Task<int> SaveChangesAsync();

  /// <summary>
  /// Begins tracking the given <see cref="Token"/>, and any other reachable entities that are not already being tracked,
  /// in the <see cref="EntityState.Added"/> state such that they will be inserted into the database when SaveChanges() is called.
  /// </summary>
  /// <param name="token">The <see cref="Token"/> to add.</param>
  /// <returns>
  /// The <see cref="EntityEntry{TEntity}"/> for the entity.
  /// The entry provides access to change tracking information and operations for the entity.
  /// </returns>
  EntityEntry<Token> AddToken(Token token);

  /// <summary>
  /// Begins tracking the given <see cref="Token"/> in the <see cref="EntityState.Deleted"/> state such that it will be removed from the database when SaveChanges() is called.
  /// </summary>
  /// <param name="token">The <see cref="Token"/> to remove.</param>
  /// <returns>
  /// The <see cref="EntityEntry{TEntity}"/> for the entity.
  /// The entry provides access to change tracking information and operations for the entity.
  /// </returns>
  EntityEntry<Token> RemoveToken(Token token);

  /// <summary>
  /// Checks if the <see cref="Token"/> with the given identification has the given user assigned to it.
  /// </summary>
  /// <param name="tokenId">The identification of the <see cref="Token"/>.</param>
  /// <param name="userId">The identification of the user.</param>
  /// <returns>true if the <see cref="Token"/> exists and belongs to the user, else false.</returns>
  bool IsTokenOwnedByUser(Guid? tokenId, Guid? userId);

  /// <summary>
  /// Checks if the <see cref="Token"/> with the given identification exists.
  /// </summary>
  /// <param name="id">The identification of the <see cref="Token"/>.</param>
  /// <returns>true if the <see cref="Token"/> exists, else false.</returns>
  bool TokenExists(Guid? id);

  /// <summary>
  /// Checks if a <see cref="Token"/> of the same user with the same token name exists.
  /// </summary>
  /// <param name="userId">The identification of the user.</param>
  /// <param name="name">The name of the <see cref="Token"/></param>
  /// <returns>true if another token of the same user with the same token name exists, else false.</returns>
  bool HasUserEquallyNamedToken(Guid? userId, string name);
}