using Microsoft.AspNetCore.Mvc;

namespace Flooq.Api.Services
{
  /// <summary>
  /// Provides for querying Flooq versions from the Flooq database.
  /// </summary>
  public interface IVersionService
  {
      /// <summary>
      /// Queries the latest Flooq version from the Flooq database.
      /// </summary>
      /// <returns>the latest Flooq version</returns>
    Task<ActionResult<Api.Model.Version>> GetLatestVersion();
  }
}