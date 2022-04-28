using Flooq.Api.Metrics.Registries;

namespace Flooq.Api.Metrics.Services;

public class LinearizedGraphMetricsService : IMetricsService
{
  public void IncrementRequestedListsCount()
  {
    LinearizedGraphRegistry.RequestedListsCount.Inc();
  }

  public void IncrementRequestedByIdCount()
  {
    LinearizedGraphRegistry.RequestedByIdCount.Inc();
  }

  public void IncrementEditedCount()
  {
    throw new NotImplementedException();
  }

  public void IncrementCreatedCount()
  {
    LinearizedGraphRegistry.CreatedCount.Inc();
  }

  public void IncrementDeletedCount()
  {
    throw new NotImplementedException();
  }

  public void IncrementNotFoundCount()
  {
    LinearizedGraphRegistry.NotFoundCount.Inc();
  }

  public void IncrementBadRequestCount()
  {
    LinearizedGraphRegistry.BadRequestCount.Inc();
  }

  public void IncrementExceptionCount()
  {
    throw new NotImplementedException();
  }
}