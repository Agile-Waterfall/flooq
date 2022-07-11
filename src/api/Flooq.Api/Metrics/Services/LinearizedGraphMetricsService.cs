using Flooq.Api.Metrics.Registries;

namespace Flooq.Api.Metrics.Services;

public class LinearizedGraphMetricsService : ILinearizedGraphMetricsService
{
  public void IncrementRequestedListsCount()
  {
    LinearizedGraphRegistry.RequestedListsCount.Inc();
  }

  public void IncrementRequestedByIdCount()
  {
    LinearizedGraphRegistry.RequestedByIdCount.Inc();
  }

  public void IncrementConflictCount()
  {
    LinearizedGraphRegistry.ConflictCount.Inc();
  }

  public void IncrementCreatedCount()
  {
    LinearizedGraphRegistry.CreatedCount.Inc();
  }

  public void IncrementNotFoundCount()
  {
    LinearizedGraphRegistry.NotFoundCount.Inc();
  }

  public void IncrementExceptionCount()
  {
    LinearizedGraphRegistry.ExceptionCount.Inc();
  }
}