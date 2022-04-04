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
    
    public async Task<ActionResult<IEnumerable<DataFlow>>> GetDataFlows() 
    { 
      return await _context.DataFlows.ToListAsync();
    }

    public async Task<ActionResult<DataFlow>> GetDataFlow(Guid id)
    {
      return await _context.DataFlows.FindAsync(id);
    }

    public void SetEntryState(DataFlow dataFlow, EntityState entityState)
    {
      _context.Entry(dataFlow).State = entityState;
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

    public bool DataFlowExists(Guid id)
    { 
      return _context.DataFlows.Any(e => e.Id == id);
    }
}