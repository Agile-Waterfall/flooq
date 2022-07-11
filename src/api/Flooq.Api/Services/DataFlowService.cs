using Flooq.Api.Domain;
using Flooq.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Flooq.Api.Services;

public class DataFlowService : IDataFlowService
{
  private readonly FlooqContext _context;

  public DataFlowService(FlooqContext context)
  {
    _context = context;
  }

  public async Task<ActionResult<IEnumerable<DataFlow>>> GetDataFlowsByUserId(Guid userId)
  {
    return await _context.DataFlows.Where(dataFlow => dataFlow.UserId.Equals(userId)).ToListAsync();
  }

  public async Task<ActionResult<DataFlow?>> GetDataFlowById(Guid? id)
  {
    var dataFlow = await _context.DataFlows.FindAsync(id);
    return new ActionResult<DataFlow?>(dataFlow);
  }

  public async Task<ActionResult<DataFlow?>> GetDataFlowByIdByUserId(Guid? id, Guid? userId)
  {
    var dataFlow = await _context.DataFlows.FindAsync(id);
    if (dataFlow != null && dataFlow.UserId.Equals(userId))
    {
      return new ActionResult<DataFlow?>(dataFlow);
    }
    return new ActionResult<DataFlow?>((DataFlow)null!);
  }

  public ActionResult<DataFlow> PutDataFlow(DataFlow dataFlow)
  {
    _context.Entry(dataFlow).State = EntityState.Modified;
    return new ActionResult<DataFlow>(dataFlow);
  }

  public async Task<int> SaveChangesAsync()
  {
    return await _context.SaveChangesAsync();
  }

  public EntityEntry<DataFlow> AddDataFlow(DataFlow dataFlow)
  {
    return _context.DataFlows.Add(dataFlow);
  }

  public EntityEntry<DataFlow> RemoveDataFlow(DataFlow dataFlow)
  {
    return _context.DataFlows.Remove(dataFlow);
  }

  public int RemoveAllDataFlowsByUserId(Guid? userId)
  {
    var dataFlowsByUser = _context.DataFlows.Where(d => d.UserId.Equals(userId)).ToArray();
    _context.DataFlows.RemoveRange(dataFlowsByUser);
    return dataFlowsByUser.Length;
  }

  public bool DataFlowExists(Guid? id)
  {
    return _context.DataFlows.Any(e => e.Id == id);
  }

  public bool IsDataFlowOwnedByUser(Guid? dataFlowId, Guid? userId)
  {
    return _context.DataFlows.Any(e => e.Id == dataFlowId && e.UserId == userId);
  }
}