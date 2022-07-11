using Flooq.Api.Metrics.Registries;
using Microsoft.AspNetCore.Mvc;

namespace Flooq.Api.Metrics.Services;

public interface ITokenMetricsService
{
  /// <summary>
  /// Increments the <see cref="TokenRegistry.RequestedListsCount"/> every time all <see cref="Models.Token"/>s
  /// of a user are requested.
  /// </summary>
  void IncrementRequestedListsCount();
  
  /// <summary>
  /// Increments the <see cref="TokenRegistry.RequestedByIdCount"/> every time a <see cref="Models.Token"/>
  /// of a user was requested by id.
  /// </summary>
  void IncrementRequestedByIdCount();
  
  /// <summary>
  /// Increments the <see cref="TokenRegistry.EditedCount"/> every time a <see cref="Models.Token"/>
  /// was edited.
  /// </summary>
  void IncrementEditedCount();
  
  /// <summary>
  /// Increments the <see cref="TokenRegistry.CreatedCount"/> every time a <see cref="Models.Token"/>
  /// was created.
  /// </summary>
  void IncrementCreatedCount();
  
  /// <summary>
  /// Increments the <see cref="TokenRegistry.DeletedCount"/> every time a <see cref="Models.Token"/>
  /// was deleted.
  /// </summary>
  /// <param name="increment">Increments the <see cref="TokenRegistry.DeletedCount"/> by this number.</param>
  void IncrementDeletedCount(double increment = 1D);
  
  /// <summary>
  /// Increments the <see cref="TokenRegistry.NotFoundCount"/> every time a <see cref="Models.Token"/>
  /// was not found.
  /// </summary>
  void IncrementNotFoundCount();
  
  /// <summary>
  /// Increments the <see cref="TokenRegistry.ConflictCount"/> every time a <see cref="ConflictResult"/>
  /// was returned in the <see cref="Controllers.TokenController"/>.
  /// </summary>
  void IncrementConflictCount();
  
  /// <summary>
  /// Increments the <see cref="TokenRegistry.BadRequestCount"/> every time a <see cref="BadHttpRequestException"/>
  /// was returned in the <see cref="Controllers.TokenController"/>.
  /// </summary>
  void IncrementBadRequestCount();
  
  /// <summary>
  /// Increments the <see cref="TokenRegistry.ExceptionCount"/> every time an exception occurred
  /// in the <see cref="Controllers.TokenController"/>.
  /// </summary>
  void IncrementExceptionCount();

  /// <summary>
  /// Increments the <see cref="TokenRegistry.UnauthorizedCount"/> every time a <see cref="UnauthorizedResult"/>
  /// was returned in the <see cref="Controllers.TokenController"/>.
  /// </summary>
  void IncrementUnauthorizedCount();
}