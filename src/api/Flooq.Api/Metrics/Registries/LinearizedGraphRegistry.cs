using Prometheus;

namespace Flooq.Api.Metrics.Registries;

public static class LinearizedGraphRegistry
{
  public static readonly Counter RequestedListsCount =
    Prometheus.Metrics.CreateCounter("requested_linearized_graph_lists", "Total number of requested linearized graph lists.");

  public static readonly Counter RequestedByIdCount =
    Prometheus.Metrics.CreateCounter("requested_linearized_graphs_by_id", "Total number of requested linearized graphs by id.");

  public static readonly Counter CreatedCount =
    Prometheus.Metrics.CreateCounter("created_linearized_graphs", "Total number of created linearized graphs.");

  public static readonly Counter NotFoundCount =
    Prometheus.Metrics.CreateCounter("linearized_graph_not_found_total",
      "Total number of linearized graphs that were requested by the api but not found in the data base.");

  public static readonly Counter BadRequestCount =
    Prometheus.Metrics.CreateCounter("linearized_graph_bad_request_total",
      "Total number of returned bad requests by the linearized graph api endpoint.");
}