using System.ComponentModel.DataAnnotations;

namespace Flooq.Api.Models
{
  public class Version
  {
    [Key]
    public string? Tag { get; set; }
    public string? Name { get; set; }
    public string? Notes { get; set; }
  }
}