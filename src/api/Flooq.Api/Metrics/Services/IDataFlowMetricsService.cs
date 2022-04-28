namespace Flooq.Api.Metrics.Services;

public interface IDataFlowMetricsService
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