using Prometheus;

namespace Flooq.Api.Metrics;

public static class LinearizedGraphRegistry
{
  public static readonly Counter RequestedGraphLists =
    Prometheus.Metrics.CreateCounter("requested_linearized_graph_lists", "HELP IS MISSING");

  public static readonly Counter RequestedGraphsById =
    Prometheus.Metrics.CreateCounter("requested_linearized_graphs_by_id", "HELP IS MISSING");

  public static readonly Counter CreatedGraphs =
    Prometheus.Metrics.CreateCounter("created_linearized_graphs", "HELP IS MISSING");
}