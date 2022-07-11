using Prometheus;

namespace Flooq.Api.Metrics.Registries;

public static class DataFlowRegistry
{
  public static readonly Counter RequestedListsCount =
    Prometheus.Metrics.CreateCounter("requested_data_flow_lists", "Total number of requested data flow lists.");
  
  public static readonly Counter RequestedByIdCount =
    Prometheus.Metrics.CreateCounter("requested_data_flows_by_id", "Total number of requested data flows by id.");
  
  public static readonly Counter EditedCount =
    Prometheus.Metrics.CreateCounter("edited_data_flows", "Total number of edited data flows.");
  
  public static readonly Counter CreatedCount =
    Prometheus.Metrics.CreateCounter("created_data_flows", "Total number of created data flows.");
  
  public static readonly Counter DeletedCount =
    Prometheus.Metrics.CreateCounter("deleted_data_flows", "Total number of deleted data flows.");

  public static readonly Counter NotFoundCount =
    Prometheus.Metrics.CreateCounter("data_flow_not_found_total",
      "Total number of data flows that were requested by the api but not found in the data base.");

  public static readonly Counter ConflictCount =
    Prometheus.Metrics.CreateCounter("data_flow_conflict_total",
      "Total number of returned conflicts by the data flow api endpoint.");

  public static readonly Counter BadRequestCount =
    Prometheus.Metrics.CreateCounter("data_flow_bad_request_total",
      "Total number of returned bad requests by the data flow api endpoint.");

  public static readonly Counter ExceptionCount =
    Prometheus.Metrics.CreateCounter("data_flow_thrown_exceptions_total",
      "Total number of thrown exceptions in the data flow api endpoint.");

  public static readonly Counter UnauthorizedCount =
    Prometheus.Metrics.CreateCounter("data_flow_unauthorized_total",
      "Total number of data flows that were requested but not owned by requester.");
}