using System.ComponentModel.DataAnnotations;

namespace Flooq.Api.Models;

public class LinearizedGraph
{
  [Key]
  public Guid Id { get; set; }
  public string? Graph { get; set; }
}