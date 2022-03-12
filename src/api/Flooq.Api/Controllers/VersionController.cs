using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Flooq.Domain;

namespace Flooq.Controllers
{
  [Route("api/version")]
  [ApiController]
  public class VersionController : ControllerBase
  {
    private readonly FlooqContext _context;

    public VersionController(FlooqContext context) {
      _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<Model.Version>> GetVersion()
    {
      var version = await _context.Versions.OrderBy(v => v.Tag).LastAsync();

      if (version == null)
      {
        return NotFound();
      }

      return version;
    }
  }
}