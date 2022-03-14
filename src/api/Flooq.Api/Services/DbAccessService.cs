using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Flooq.Domain;

namespace Flooq.Services
{
  public class DbAccessService
  {
    private readonly FlooqContext _context;

    public DbAccessService() {}

    public DbAccessService(FlooqContext context)
    {
      _context = context;
    }

    public virtual async Task<ActionResult<Model.Version>> GetLatestVersion()
    {
      return await _context.Versions.OrderBy(v => v.Tag).LastAsync();
    }
  }
}