using Prometheus;

namespace Flooq.Api.Metrics.Registries;

public static class ContactRegistry
{
  public static readonly Counter RequestedListsCount =
    Prometheus.Metrics.CreateCounter("requested_contact_lists", "Total number of requested contact lists.");
  
  public static readonly Counter CreatedCount =
    Prometheus.Metrics.CreateCounter("created_contacts", "Total number of created contacts.");
  
  public static readonly Counter DeletedCount =
    Prometheus.Metrics.CreateCounter("deleted_contacts", "Total number of deleted contacts.");
  
  public static readonly Counter NotFoundCount =
    Prometheus.Metrics.CreateCounter("contact_not_found_total",
      "Total number of contacts that were requested by the api but not found in the data base.");

  public static readonly Counter BadRequestCount =
    Prometheus.Metrics.CreateCounter("contact_bad_request_total",
      "Total number of returned bad requests by the contact api endpoint.");

  public static readonly Counter ExceptionCount =
    Prometheus.Metrics.CreateCounter("contact_thrown_exceptions_total",
      "Total number of thrown exceptions in the contact api endpoint.");
}