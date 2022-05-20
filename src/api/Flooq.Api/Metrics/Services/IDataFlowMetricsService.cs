using Flooq.Api.Metrics.Registries;

namespace Flooq.Api.Metrics.Services;

public interface IDataFlowMetricsService
{
  /// <summary>
  /// Increments the <see cref="DataFlowRegistry.RequestedListsCount"/> every time all <see cref="Models.DataFlow"/>s
  /// of a user are requested.
  /// </summary>
  void IncrementRequestedListsCount();
  
  /// <summary>
  /// Increments the <see cref="DataFlowRegistry.RequestedByIdCount"/> every time a <see cref="Models.DataFlow"/>
  /// of a user was requested by id.
  /// </summary>
  void IncrementRequestedByIdCount();
  
  /// <summary>
  /// Increments the <see cref="DataFlowRegistry.EditedCount"/> every time a <see cref="Models.DataFlow"/>
  /// was edited.
  /// </summary>
  void IncrementEditedCount();
  
  /// <summary>
  /// Increments the <see cref="DataFlowRegistry.CreatedCount"/> every time a <see cref="Models.DataFlow"/>
  /// was created.
  /// </summary>
  void IncrementCreatedCount();
  
  /// <summary>
  /// Increments the <see cref="DataFlowRegistry.DeletedCount"/> every time a <see cref="Models.DataFlow"/>
  /// was deleted.
  /// </summary>
  void IncrementDeletedCount();
  
  /// <summary>
  /// Increments the <see cref="DataFlowRegistry.NotFoundCount"/> every time a <see cref="Models.DataFlow"/>
  /// was not found.
  /// </summary>
  void IncrementNotFoundCount();
  
  /// <summary>
  /// Increments the <see cref="DataFlowRegistry.BadRequestCount"/> every time a <see cref="BadHttpRequestException"/>
  /// was returned in the <see cref="Controllers.DataFlowController"/>.
  /// </summary>
  void IncrementBadRequestCount();
  
  /// <summary>
  /// Increments the <see cref="DataFlowRegistry.ExceptionCount"/> every time an exception occurred
  /// in the <see cref="Controllers.DataFlowController"/>.
  /// </summary>
  void IncrementExceptionCount();
}