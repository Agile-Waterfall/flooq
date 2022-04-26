using Prometheus;

namespace Flooq.Api.Metrics;

public static class DataFlowRegistry
{
  public static readonly Counter RequestedDataFlowLists =
    Prometheus.Metrics.CreateCounter("requested_data_flow_lists", "HELP IS MISSING");
  
  public static readonly Counter RequestedDataFlowsById =
    Prometheus.Metrics.CreateCounter("requested_data_flows_by_id", "HELP IS MISSING");
  
  public static readonly Counter EditedDataFlows =
    Prometheus.Metrics.CreateCounter("edited_data_flows", "HELP IS MISSING");
  
  public static readonly Counter CreatedDataFlows =
    Prometheus.Metrics.CreateCounter("created_data_flows", "HELP IS MISSING");
  
  public static readonly Counter DeletedDataFlows =
    Prometheus.Metrics.CreateCounter("deleted_data_flows", "HELP IS MISSING");
}