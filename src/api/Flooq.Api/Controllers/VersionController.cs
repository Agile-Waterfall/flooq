using Microsoft.AspNetCore.Mvc;
using Flooq.Services;

namespace Flooq.Controllers
{
  [Route("api/version")]
  [ApiController]
  public class VersionController : ControllerBase
  {
    private readonly IVersionService versionService;

    public VersionController(IVersionService versionService) 
    {
      this.versionService = versionService;
    }
    
    [HttpGet]
    public async Task<ActionResult<Model.Version>> GetVersion()
    {
      var version = await versionService.GetLatestVersion();

      if (version == null)
      {
        return NotFound();
      }

      return version;
    }
  }
}