using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Flooq.Domain;

namespace Flooq.Services
{
  public class VersionService : IVersionService
  {
    private readonly FlooqContext _context;

    public VersionService(FlooqContext context)
    {
      _context = context;
    }

    public async Task<ActionResult<Model.Version>> GetLatestVersion()
    {
      return await _context.Versions.OrderBy(v => v.Tag).LastAsync();
    }
  }
}