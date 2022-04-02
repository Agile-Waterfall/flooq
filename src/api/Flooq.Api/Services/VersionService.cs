using Flooq.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Flooq.Api.Services
{
  public class VersionService : IVersionService
  {
    private readonly FlooqContext _context;

    public VersionService(FlooqContext context)
    {
      _context = context;
    }

    public async Task<ActionResult<Api.Model.Version>> GetLatestVersion()
    {
      return await _context.Versions.OrderBy(v => v.Tag).LastAsync();
    }
  }
}