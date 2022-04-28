namespace Flooq.Api.Metrics.Services;

public interface IMetricsService
{
  void IncrementRequestedListsCount();
  void IncrementRequestedByIdCount();
  void IncrementEditedCount();
  void IncrementCreatedCount();
  void IncrementDeletedCount();
  void IncrementNotFoundCount();
  void IncrementBadRequestCount();
  void IncrementExceptionCount();
}