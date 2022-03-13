using Microsoft.AspNetCore.Mvc;
using Flooq.Services;

namespace Flooq.Controllers
{
  [Route("api/version")]
  [ApiController]
  public class VersionController : ControllerBase
  {
    private readonly DbAccessService _dbAccessService;

    public VersionController(DbAccessService dbAccessService) 
    {
      _dbAccessService = dbAccessService;
    }
    
    [HttpGet]
    public async Task<ActionResult<Model.Version>> GetVersion()
    {
      var version = await _dbAccessService.GetLatestVersion();

      if (version == null)
      {
        return NotFound();
      }

      return version;
    }
  }
}