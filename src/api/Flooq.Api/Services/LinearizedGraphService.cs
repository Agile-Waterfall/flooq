using Flooq.Api.Domain;
using Flooq.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Flooq.Api.Services;

public class LinearizedGraphService : ILinearizedGraphService
{
  private readonly FlooqContext _context;

  public LinearizedGraphService(FlooqContext context)
  {
    _context = context;
  }
    
  public async Task<ActionResult<IEnumerable<LinearizedGraph>>> GetGraphs()
  {
    return await _context.Graphs.ToListAsync();
  }

  public async Task<ActionResult<LinearizedGraph?>> GetGraph(Guid id)
  {
    var graph = await _context.Graphs.FindAsync(id);
    return new ActionResult<LinearizedGraph?>(graph);
  }

  public async Task<int> SaveChangesAsync()
  {
    return await _context.SaveChangesAsync();
  }

  public EntityEntry<LinearizedGraph> AddGraph(LinearizedGraph graph)
  {
    return _context.Graphs.Add(graph);
  }

  public EntityEntry<LinearizedGraph> RemoveGraph(LinearizedGraph graph)
  {
    return _context.Graphs.Remove(graph);
  }

  public bool LinearizedGraphExists(Guid id)
  {
    return _context.Graphs.Any(e => e.Id == id);
  }
}