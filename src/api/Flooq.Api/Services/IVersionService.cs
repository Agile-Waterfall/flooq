using Microsoft.AspNetCore.Mvc;

namespace Flooq.Services
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
    Task<ActionResult<Model.Version>> GetLatestVersion();
  }
}