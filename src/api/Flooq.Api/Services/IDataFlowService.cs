using Flooq.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Flooq.Api.Services;

/// <summary>
/// Provides for querying Flooq data flows from the Flooq database.
/// </summary>
public interface IDataFlowService
{ 
  /// <summary>
  /// Queries the all Data Flows from the Flooq database.
  /// </summary>
  /// <returns>all Data Flows</returns>
  Task<ActionResult<IEnumerable<DataFlow>>> GetDataFlows();

  Task<ActionResult<DataFlow>> GetDataFlow(Guid? id);
  
  void SetEntryState(DataFlow dataFlow, EntityState entityState);
  
  Task<int> SaveChangesAsync();

  public EntityEntry<DataFlow> AddDataFlow(DataFlow dataFlow);

  public EntityEntry<DataFlow> RemoveDataFlow(DataFlow dataFlow);

  public bool DataFlowExists(Guid? id);
}