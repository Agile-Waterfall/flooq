using App.Metrics;
using App.Metrics.Counter;

namespace Flooq.Api.Metrics;

public static class DataFlowRegistry
{
  public static CounterOptions RequestedDataFlowListsCounter => new()
  {
    Name = "Requested Data Flow Lists",
    Context = "FlooqApi",
    MeasurementUnit = Unit.Calls
  };
  
  public static CounterOptions RequestedDataFlowsByIdCounter => new()
  {
    Name = "Requested Data Flows By Id",
    Context = "FlooqApi",
    MeasurementUnit = Unit.Calls
  };

  public static CounterOptions EditedDataFlowsCounter => new()
  {
    Name = "Edited Data Flows",
    Context = "FlooqApi",
    MeasurementUnit = Unit.Calls
  };
  
  public static CounterOptions CreatedDataFlowsCounter => new()
  {
    Name = "Created Data Flows",
    Context = "FlooqApi",
    MeasurementUnit = Unit.Calls
  };
  
  public static CounterOptions DeletedDataFlowsCounter => new()
  {
    Name = "Deleted Data Flows",
    Context = "FlooqApi",
    MeasurementUnit = Unit.Calls
  };
}