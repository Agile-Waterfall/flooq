using Microsoft.AspNetCore.Mvc;

namespace Flooq.Controllers
{
  [Route("api/status")]
  [ApiController]
  public class StatusController
  {
    [HttpGet]
    public string GetStatus()
    {
      return "running";
    }
  }
}