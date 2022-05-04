using Flooq.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Flooq.Api.Services;

/// <summary>
/// Provides for querying Flooq <see cref="DataFlow"/>s from the Flooq database.
/// </summary>
public interface IDataFlowService
{
  /// <summary>
  /// Queries all <see cref="DataFlow"/>s for the user having the given id from the Flooq database.
  /// </summary>
  /// <returns>The identified <see cref="DataFlow"/>.</returns>
  /// <param name="userId">The id of the user.</param>
  Task<ActionResult<IEnumerable<DataFlow>>> GetDataFlowsByUserId(Guid userId);

  /// <summary>
  /// Finds a specific <see cref="DataFlow"/> in the Flooq database according to a unique identification.
  /// </summary>
  /// <returns>The identified <see cref="DataFlow"/>.</returns>
  /// <param name="id">The value which is used for identifying the <see cref="DataFlow"/>.</param>
  Task<ActionResult<DataFlow?>> GetDataFlowById(Guid? id);

  /// <summary>
  /// Finds a specific <see cref="DataFlow"/> belonging to a specific user according to a unique
  /// identification.
  /// </summary>
  /// <returns>The identified <see cref="DataFlow"/>.</returns>
  /// <param name="id">The value which is used for identifying the <see cref="DataFlow"/>.</param>
  /// <param name="userId">The value which is used for identifying the user.</param>
  Task<ActionResult<DataFlow?>> GetDataFlowByIdByUserId(Guid? id, Guid? userId);
  
  /// <summary>
  /// The existing <see cref="DataFlow"/> entry in the database will be overwritten by the new <see cref="DataFlow"/> when a save operation on the
  /// associated <see cref="DbContext"/> is called afterwards.
  /// </summary>
  /// <param name="dataFlow">The new <see cref="DataFlow"/>.</param>
  ActionResult<DataFlow> PutDataFlow(DataFlow dataFlow);

  /// <summary>
  /// Saves all previously made changes in this <see cref="DbContext"/> to the database.
  /// </summary>
  /// <returns>
  /// A <see cref="Task"/> that represents the asynchronous save operation.
  /// The <see cref="Task"/> result contains the number of state entries written to the database.
  /// </returns>
  Task<int> SaveChangesAsync();
  
  /// <summary>
  /// Begins tracking the given <see cref="DataFlow"/>, and any other reachable entities that are not already being tracked,
  /// in the <see cref="EntityState.Added"/> state such that they will be inserted into the database when SaveChanges() is called.
  /// </summary>
  /// <param name="dataFlow">The <see cref="DataFlow"/> to add.</param>
  /// <returns>
  /// The <see cref="EntityEntry{TEntity}"/> for the entity.
  /// The entry provides access to change tracking information and operations for the entity.
  /// </returns>
  EntityEntry<DataFlow> AddDataFlow(DataFlow dataFlow);

  /// <summary>
  /// Begins tracking the given <see cref="DataFlow"/> in the <see cref="EntityState.Deleted"/> state such that it will be removed from the database when SaveChanges() is called.
  /// </summary>
  /// <param name="dataFlow">The <see cref="DataFlow"/> to remove.</param>
  /// <returns>
  /// The <see cref="EntityEntry{TEntity}"/> for the entity.
  /// The entry provides access to change tracking information and operations for the entity.
  /// </returns>
  EntityEntry<DataFlow> RemoveDataFlow(DataFlow dataFlow);

  /// <summary>
  /// Checks if the <see cref="DataFlow"/> with the given identification exists.
  /// </summary>
  /// <param name="id">The identification of the <see cref="DataFlow"/>.</param>
  /// <returns>true if the <see cref="DataFlow"/> exists, else false.</returns>
  bool DataFlowExists(Guid? id);

  /// <summary>
  /// Checks if the <see cref="DataFlow"/> with the given identification has the given user assigned to it.
  /// </summary>
  /// <param name="id">The identification of the <see cref="DataFlow"/>.</param>
  /// <returns>true if the <see cref="DataFlow"/> exists and belongs to the user, else false.</returns>
  bool IsDataFlowOwnedByUser(Guid? dataFlowId, Guid? userId);
}