using System.ComponentModel.DataAnnotations;

namespace Flooq.Api.Model
{
  public class Version
  {
    [Key]
    public string? Tag { get; set; }
    public string? Name { get; set; }
    public string? Notes { get; set; }
  }
}