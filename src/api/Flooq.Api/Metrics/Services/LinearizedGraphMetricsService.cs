using Flooq.Api.Metrics.Registries;

namespace Flooq.Api.Metrics.Services;

public class LinearizedGraphDataFlowMetricsService : ILinearizedGraphMetricsService
{
  public void IncrementRequestedListsCount()
  {
    LinearizedGraphRegistry.RequestedListsCount.Inc();
  }

  public void IncrementRequestedByIdCount()
  {
    LinearizedGraphRegistry.RequestedByIdCount.Inc();
  }

  public void IncrementCreatedCount()
  {
    LinearizedGraphRegistry.CreatedCount.Inc();
  }

  public void IncrementNotFoundCount()
  {
    LinearizedGraphRegistry.NotFoundCount.Inc();
  }

  public void IncrementBadRequestCount()
  {
    LinearizedGraphRegistry.BadRequestCount.Inc();
  }
}