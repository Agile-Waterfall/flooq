using Microsoft.AspNetCore.Mvc;

namespace Flooq.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class VersionController : ControllerBase
  {
    [HttpGet(Name = "GetVersion")]
    public string Get()
    {
      return "v0.1.1";
    }
  }
}