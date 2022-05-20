using Flooq.Api.Metrics.Registries;
using Microsoft.AspNetCore.Mvc;

namespace Flooq.Api.Metrics.Services;

public interface IContactMetricsService
{
  /// <summary>
  /// Increments the <see cref="ContactRegistry.RequestedListsCount"/> every time all <see cref="Models.Contact"/>s
  /// are requested.
  /// </summary>
  void IncrementRequestedListsCount();
  
  /// <summary>
  /// Increments the <see cref="ContactRegistry.CreatedCount"/> every time a <see cref="Models.Contact"/>
  /// was created.
  /// </summary>
  void IncrementCreatedCount();
  
  /// <summary>
  /// Increments the <see cref="ContactRegistry.DeletedCount"/> every time a <see cref="Models.Contact"/>
  /// was deleted.
  /// </summary>
  void IncrementDeletedCount();
  
  /// <summary>
  /// Increments the <see cref="ContactRegistry.NotFoundCount"/> every time a <see cref="Models.Contact"/>
  /// was not found.
  /// </summary>
  void IncrementNotFoundCount();
  
  /// <summary>
  /// Increments the <see cref="ContactRegistry.ConflictCount"/> every time a <see cref="ConflictResult"/>
  /// was returned in the <see cref="Controllers.ContactController"/>.
  /// </summary>
  void IncrementConflictCount();
  
  /// <summary>
  /// Increments the <see cref="ContactRegistry.BadRequestCount"/> every time a <see cref="BadHttpRequestException"/>
  /// was returned in the <see cref="Controllers.ContactController"/>.
  /// </summary>
  void IncrementBadRequestCount();
  
  /// <summary>
  /// Increments the <see cref="ContactRegistry.ExceptionCount"/> every time an exception occurred
  /// in the <see cref="Controllers.ContactController"/>.
  /// </summary>
  void IncrementExceptionCount();
}