using Prometheus;

namespace Flooq.Api.Metrics.Registries;

public static class TokenRegistry
{
  public static readonly Counter RequestedListsCount =
    Prometheus.Metrics.CreateCounter("requested_token_lists", "Total number of requested token lists.");
  
  public static readonly Counter RequestedByIdCount =
    Prometheus.Metrics.CreateCounter("requested_tokens_by_id", "Total number of requested tokens by id.");
  
  public static readonly Counter EditedCount =
    Prometheus.Metrics.CreateCounter("edited_tokens", "Total number of edited tokens.");
  
  public static readonly Counter CreatedCount =
    Prometheus.Metrics.CreateCounter("created_tokens", "Total number of created tokens.");
  
  public static readonly Counter DeletedCount =
    Prometheus.Metrics.CreateCounter("deleted_tokens", "Total number of deleted tokens.");

  public static readonly Counter NotFoundCount =
    Prometheus.Metrics.CreateCounter("token_not_found_total",
      "Total number of tokens that were requested by the api but not found in the data base.");

  public static readonly Counter ConflictCount =
    Prometheus.Metrics.CreateCounter("token_conflict_total",
      "Total number of returned conflicts by the token api endpoint.");

  public static readonly Counter BadRequestCount =
    Prometheus.Metrics.CreateCounter("token_bad_request_total",
      "Total number of returned bad requests by the token api endpoint.");

  public static readonly Counter ExceptionCount =
    Prometheus.Metrics.CreateCounter("token_thrown_exceptions_total",
      "Total number of thrown exceptions in the token api endpoint.");

  public static readonly Counter UnauthorizedCount =
    Prometheus.Metrics.CreateCounter("token_unauthorized_total",
      "Total number of tokens that were requested but not owned by requester.");
}