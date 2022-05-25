using Flooq.Api.Metrics.Registries;
using Microsoft.AspNetCore.Mvc;

namespace Flooq.Api.Metrics.Services;

public interface ILinearizedGraphMetricsService
{
  /// <summary>
  /// Increments the <see cref="LinearizedGraphRegistry.RequestedListsCount"/> every time all <see cref="Models.LinearizedGraph"/>s
  /// of a user are requested.
  /// </summary>
  void IncrementRequestedListsCount();
  
  /// <summary>
  /// Increments the <see cref="LinearizedGraphRegistry.RequestedByIdCount"/> every time a <see cref="Models.LinearizedGraph"/>
  /// of a user was requested by id.
  /// </summary>
  void IncrementRequestedByIdCount();
  
  /// <summary>
  /// Increments the <see cref="LinearizedGraphRegistry.CreatedCount"/> every time a <see cref="Models.LinearizedGraph"/>
  /// was created.
  /// </summary>
  void IncrementCreatedCount();
  
  /// <summary>
  /// Increments the <see cref="LinearizedGraphRegistry.NotFoundCount"/> every time a <see cref="Models.LinearizedGraph"/>
  /// was not found.
  /// </summary>
  void IncrementNotFoundCount();
  
  /// <summary>
  /// Increments the <see cref="LinearizedGraphRegistry.ConflictCount"/> every time a <see cref="ConflictResult"/>
  /// was returned in the <see cref="Controllers.LinearizedGraphController"/>.
  /// </summary>
  void IncrementConflictCount();
  
  /// <summary>
  /// Increments the <see cref="LinearizedGraphRegistry.ExceptionCount"/> every time an exception occurred
  /// in the <see cref="Controllers.LinearizedGraphController"/>.
  /// </summary>
  void IncrementExceptionCount();
}