using Flooq.Api.Metrics.Registries;

namespace Flooq.Api.Metrics.Services;

public class ContactMetricsService : IContactMetricsService
{
  public void IncrementRequestedListsCount()
  {
    ContactRegistry.RequestedListsCount.Inc();
  }
  
  public void IncrementCreatedCount()
  {
    ContactRegistry.CreatedCount.Inc();
  }

  public void IncrementDeletedCount()
  {
    ContactRegistry.DeletedCount.Inc();
  }

  public void IncrementNotFoundCount()
  {
    ContactRegistry.NotFoundCount.Inc();
  }

  public void IncrementConflictCount()
  {
    ContactRegistry.ConflictCount.Inc();
  }

  public void IncrementBadRequestCount()
  {
    ContactRegistry.BadRequestCount.Inc();
  }

  public void IncrementExceptionCount()
  {
    ContactRegistry.ExceptionCount.Inc();
  }
}