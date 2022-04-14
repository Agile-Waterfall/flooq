using System.ComponentModel.DataAnnotations;

namespace Flooq.Api.Models
{
  public class DataFlow
  {
    [Key]
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string? Status { get; set; }
    public DateTime? LastEdited { get; set; }
    public string? Definition { get; set; }
  }
}