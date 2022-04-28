using Flooq.Api.Metrics.Registries;

namespace Flooq.Api.Metrics.Services;

public class DataFlowMetricsService : IMetricsService
{
  public void IncrementRequestedListsCount()
  {
    DataFlowRegistry.RequestedListsCount.Inc();
  }

  public void IncrementRequestedByIdCount()
  {
    DataFlowRegistry.RequestedByIdCount.Inc();
  }

  public void IncrementEditedCount()
  {
    DataFlowRegistry.EditedCount.Inc();
  }

  public void IncrementCreatedCount()
  {
    DataFlowRegistry.CreatedCount.Inc();
  }

  public void IncrementDeletedCount()
  {
    DataFlowRegistry.DeletedCount.Inc();
  }

  public void IncrementNotFoundCount()
  {
    DataFlowRegistry.NotFoundCount.Inc();
  }

  public void IncrementBadRequestCount()
  {
    DataFlowRegistry.BadRequestCount.Inc();
  }

  public void IncrementExceptionCount()
  {
    DataFlowRegistry.ExceptionCount.Inc();
  }
}