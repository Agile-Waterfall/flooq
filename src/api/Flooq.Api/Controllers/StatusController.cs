using Microsoft.AspNetCore.Mvc;

namespace Flooq.Api.Controllers
{
  [Route("api/status")]
  [ApiController]
  public class StatusController
  {
    // GET: api/status
    /// <summary>
    /// Gets the current status of the application.
    /// </summary>
    /// <returns>The status of the application.</returns>
    [HttpGet]
    public string GetStatus()
    {
      return "running";
    }
  }
}