using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Flooq.Api.Controllers
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