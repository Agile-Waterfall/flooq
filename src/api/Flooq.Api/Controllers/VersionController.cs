using Flooq.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Flooq.Api.Controllers
{
  [Route("api/version")]
  [ApiController]
  public class VersionController : ControllerBase
  {
    private readonly IVersionService _versionService;

    public VersionController(IVersionService versionService)
    {
      _versionService = versionService;
    }

    [HttpGet]
    public async Task<ActionResult<Models.Version>> GetVersion()
    {
      var version = await _versionService.GetLatestVersion();

      return version == null ? NotFound() : version;
    }
  }
}