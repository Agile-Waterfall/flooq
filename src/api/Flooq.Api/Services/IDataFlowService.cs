using Flooq.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Flooq.Api.Services;

/// <summary>
/// Provides for querying Flooq data flows from the Flooq database.
/// </summary>
public interface IDataFlowService
{ 
  /// <summary>
  /// Queries the all DataFlows from the Flooq database.
  /// </summary>
  /// <returns>All DataFlows from the Flooq database.</returns>
  Task<ActionResult<IEnumerable<DataFlow>>> GetDataFlows();

  /// <summary>
  /// Finds a specific DataFlow in the Flooq database according to a unique identification.
  /// </summary>
  /// <returns>The identified DataFlow.</returns>
  /// <param name="id">The value which is used for identifying the DataFlow.</param>
  Task<ActionResult<DataFlow>> GetDataFlow(Guid? id);
  
  /// <summary>
  /// The existing DataFlow entry in the database will be overwritten by the new DataFlow when a save operation on the
  /// associated DbContext is called afterwards.
  /// </summary>
  /// <param name="dataFlow">The new DataFlow.</param>
  void PutDataFlow(DataFlow dataFlow);

  /// <summary>
  /// Saves all previously made changes in this DbContext to the database.
  /// </summary>
  /// <returns>
  /// A task that represents the asynchronous save operation.
  /// The task result contains the number of state entries written to the database.
  /// </returns>
  Task<int> SaveChangesAsync();
  
  /// <summary>
  /// Begins tracking the given DataFlow, and any other reachable entities that are not already being tracked,
  /// in the Added state such that they will be inserted into the database when SaveChanges() is called.
  /// </summary>
  /// <param name="dataFlow">The DataFlow to add.</param>
  /// <returns>
  /// The EntityEntry<DataFlow> for the entity.
  /// The entry provides access to change tracking information and operations for the entity.
  /// </returns>
  public EntityEntry<DataFlow> AddDataFlow(DataFlow dataFlow);

  /// <summary>
  /// Begins tracking the given DataFlow in the Deleted state such that it will be removed from the database when SaveChanges() is called.
  /// </summary>
  /// <param name="dataFlow">The DataFlow to Remove.</param>
  /// <returns>
  /// The EntityEntry<DataFlow> for the entity.
  /// The entry provides access to change tracking information and operations for the entity.
  /// </returns>
  public EntityEntry<DataFlow> RemoveDataFlow(DataFlow dataFlow);

  /// <summary>
  /// Checks if the DataFlow with the given identification exists.
  /// </summary>
  /// <param name="id">The identification of the DataFlow.</param>
  /// <returns>true if the DataFlow exists, else false.</returns>
  public bool DataFlowExists(Guid? id);
}