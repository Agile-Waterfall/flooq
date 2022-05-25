using Flooq.Api.Metrics.Registries;

namespace Flooq.Api.Metrics.Services;

public class DataFlowMetricsService : IDataFlowMetricsService
{
  public void IncrementRequestedListsCount()
  {
    DataFlowRegistry.RequestedListsCount.Inc();
  }

  public void IncrementRequestedByIdCount()
  {
    DataFlowRegistry.RequestedByIdCount.Inc();
  }

  public void IncrementConflictCount()
  {
    DataFlowRegistry.ConflictCount.Inc();
  }

  public void IncrementUnauthorizedCount()
  {
    DataFlowRegistry.UnauthorizedCount.Inc();
  }

  public void IncrementEditedCount()
  {
    DataFlowRegistry.EditedCount.Inc();
  }

  public void IncrementCreatedCount()
  {
    DataFlowRegistry.CreatedCount.Inc();
  }

  public void IncrementDeletedCount(double increment = 1D)
  {
    DataFlowRegistry.DeletedCount.Inc(increment);
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