namespace Flooq.Api.Metrics.Services;

public interface ILinearizedGraphMetricsService
{
  void IncrementRequestedListsCount();
  void IncrementRequestedByIdCount();
  void IncrementCreatedCount();
  void IncrementNotFoundCount();
  void IncrementBadRequestCount();
}