using Flooq.Api.Metrics.Registries;

namespace Flooq.Api.Metrics.Services;

public class TokenMetricsService : ITokenMetricsService
{
  public void IncrementRequestedListsCount()
  {
    TokenRegistry.RequestedListsCount.Inc();
  }

  public void IncrementRequestedByIdCount()
  {
    TokenRegistry.RequestedByIdCount.Inc();
  }

  public void IncrementEditedCount()
  {
    TokenRegistry.EditedCount.Inc();
  }

  public void IncrementCreatedCount()
  {
    TokenRegistry.CreatedCount.Inc();
  }

  public void IncrementDeletedCount(double increment = 1)
  {
    TokenRegistry.DeletedCount.Inc(increment);
  }

  public void IncrementNotFoundCount()
  {
    TokenRegistry.NotFoundCount.Inc();
  }

  public void IncrementConflictCount()
  {
    TokenRegistry.ConflictCount.Inc();
  }

  public void IncrementBadRequestCount()
  {
    TokenRegistry.BadRequestCount.Inc();
  }

  public void IncrementExceptionCount()
  {
    TokenRegistry.ExceptionCount.Inc();
  }

  public void IncrementUnauthorizedCount()
  {
    TokenRegistry.UnauthorizedCount.Inc();
  }
}