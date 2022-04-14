using Flooq.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Flooq.Api.Services;

public class LinearizedGraphService : ILinearizedGraphService
{
  public Task<ActionResult<IEnumerable<LinearizedGraph>>> GetGraphs()
  {
    throw new NotImplementedException();
  }

  public Task<ActionResult<LinearizedGraph>> GetGraph(Guid id)
  {
    throw new NotImplementedException();
  }

  public Task<int> SaveChangesAsync()
  {
    throw new NotImplementedException();
  }

  public EntityEntry<LinearizedGraph> AddGraph(LinearizedGraph graph)
  {
    throw new NotImplementedException();
  }

  public EntityEntry<LinearizedGraph> RemoveGraph(LinearizedGraph graph)
  {
    throw new NotImplementedException();
  }

  public bool LinearizedGraphExists(Guid id)
  {
    throw new NotImplementedException();
  }
}